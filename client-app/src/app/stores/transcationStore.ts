import { makeAutoObservable, runInAction } from "mobx";
import { store } from "./store";
import { Transcations } from "../models/transcations";
import agent from "../api/agent";

export default class TranscationStore {
    transcations: Transcations [] = [];

    constructor() {
        makeAutoObservable(this);
    }

    getTransaction = async () => {
        try {
            const transcations = await agent.Transaction.get(store.userStore.user?.username!);
            runInAction(() => this.transcations = transcations);
        } catch (error) {
            console.log(error);
        }
    }
}

