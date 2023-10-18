import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { ChangePasswordValues, User, UserFormValues } from "../models/user";
import { store } from "./store";
import { router } from "../router/Route";

export default class UserStore {
    user: User | null = null;
    users: User[] = [];

    constructor() {
        makeAutoObservable(this);        
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            router.navigate('/activities');
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    register = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            router.navigate('/activities');
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    changepassword = async (creds: ChangePasswordValues) => {
        try {
            await agent.Account.changepassword(creds);
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate('/');
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }

    setImage = (image: string) => {
        if (this.user) this.user.image = image;
    }

    setUserPhoto = (url: string) => {
        if (this.user) this.user.image = url;
    }

    setDisplayName = (name: string) => {
        if (this.user) this.user.displayName = name;
    }

    getallusers = async () => {
        try {
            const users = await agent.Account.users();
            runInAction(() => this.users = users);
        } catch (error) {
            console.log(error);            
        }
    }

    setuseramount = async (amount: number, username: string) => {
        try {
            await agent.Account.setamount(username, amount);
        } catch (error) {
            throw(error);
        }
    }
}