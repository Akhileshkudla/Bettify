import { makeAutoObservable, runInAction } from "mobx";
import { User, UserFormValues } from "../models/user";
import agent from "../api/agent";
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

    logout = () => {
        store.commonStore.setToken(null);
        runInAction(() => this.user = null);
        router.navigate('/');
    }

    getuser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }

    getallusers = async () => {
        try {
            const users = await agent.Account.users();
            runInAction(() => this.users = users);
        } catch (error) {
            
        }
    }

    register = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            router.navigate('/activities');
            store.modalStore.closeModal();
            console.log(user);
        } catch (error) {
            throw(error);
        }
    }

    setuseramount = async (amount: number) => {
        try {
            console.log("1: ", amount)
            await agent.Account.setamount( amount);
        } catch (error) {
            throw(error);
        }
    }
}