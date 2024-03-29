import axios, { AxiosError, AxiosResponse } from 'axios';
import { Activity, ActivityFormValues } from '../models/activity';
import { toast } from 'react-toastify';
import { router } from '../router/Route';
import { store } from '../stores/store';
import { ChangePasswordValues, User, UserFormValues } from '../models/user';
import { Photo, Profile, UserActivity } from '../models/profile';
import { PaginatedResult } from '../models/pagination';
import { Transcations } from '../models/transcations';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = import.meta.env.VITE_API_URL;


axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if(token && config.headers) config.headers.Authorization = `Bearer ${token}`;
    return config;
})


axios.interceptors.response.use(async response => {   
        if( import.meta.env.DEV) await sleep(1000);
        const pagination = response.headers['pagination'];
        if (pagination) {
            response.data = new PaginatedResult(response.data, JSON.parse(pagination));
            return response as AxiosResponse<PaginatedResult<any>>
        }
        return response;    
}, (error: AxiosError) => {
    const {data, status, config} = error.response as AxiosResponse
    switch(status){
        case 400:
            if(config.method === 'get' && data.errors.hasOwnProperty('id')){
                router.navigate('/not-found')
            }
            if(data.errors) {
                const modalStateErrors = [];
                for(const key in data.errors)
                {
                    if(data.errors[key]){
                        modalStateErrors.push(data.errors[key]);
                    }
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 403:
            toast.error('forbidden');
            break;
        case 404:
            router.navigate('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error')
            break;
        default:
            break;
    }
    return Promise.reject(error);
})

const reponseBody = <T> (response : AxiosResponse<T>) => response.data;

const requests = {
    get : <T> (url: string) => axios.get<T>(url).then(reponseBody),
    post : <T> (url: string, body: {}) => axios.post<T>(url, body).then(reponseBody),
    put : <T> (url: string, body: {}) => axios.put<T>(url, body).then(reponseBody),
    del : <T> (url: string) => axios.delete<T>(url).then(reponseBody),
}

const Activities = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<Activity[]>>('/activities', { params })
        .then(reponseBody),
    details: (id: string)=> requests.get<Activity>(`/activities/${id}`),
    create: (activity: ActivityFormValues) => requests.post('/activities', activity),
    update: (activity: ActivityFormValues) => requests.put(`/activities/${activity.id}`, activity),
    delete: (id : string) => requests.del(`/activities/${id}`) ,
    attend: (id: string, option: string) => requests.post(`/activities/${id}/attend`, {option}),
    setwinningteam: (id: string, option: string) => requests.post(`/activities/${id}/setwinningteam`, {option})
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user : UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user),
    changepassword: (cred: ChangePasswordValues) => requests.post('/account/change-password', cred),
    setamount: (username: string, amount: number) => requests.put<number>('/account/setamount', { amount, username }),
    users: () => requests.get<User[]>('/account/users')
}

const Profiles = {
    get: (username: string) => requests.get<Profile>(`/profiles/${username}`),
    uploadPhoto: (file: any) => {
        let formData = new FormData();
        formData.append('File', file);
        return axios.post<Photo>('photos', formData, {
            headers: { 'Content-Type': 'multipart/form-data' }
        })
    },
    setMainPhoto: (id: string) => axios.post(`/photos/${id}/setMain`, {}),
    deletePhoto: (id: string) => axios.delete(`/photos/${id}`),
    updateProfile: (profile: Partial<Profile>) => requests.put(`/profiles`, profile),
    updateFollowing: (username: string) => requests.post(`/follow/${username}`, {}),
    listFollowings: (username: string, predicate: string) => requests
        .get<Profile[]>(`/follow/${username}?predicate=${predicate}`),
    listActivities: (username: string, predicate: string) =>
        requests.get<UserActivity[]>(`/profiles/${username}/activities?predicate=${predicate}`)
}

const Transaction = {
    get: (username: string) => requests.get<Transcations []>(`transaction/${username}`)
}

const agent = {
    Activities,
    Account,
    Profiles,
    Transaction
}

export default agent;