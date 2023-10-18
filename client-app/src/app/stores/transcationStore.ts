import { makeAutoObservable, runInAction } from "mobx";
import { store } from "./store";
import { Transcations } from "../models/transcations";
import agent from "../api/agent";

export default class TranscationStore {
    transcation: Transcations = { messages: [] };

    constructor() {
        makeAutoObservable(this);
    }

    getTransaction = async () => {
        try {
            const transcation = await agent.Transaction.get(store.userStore.user?.username!);
            runInAction(() => this.transcation.messages = transcation);
        } catch (error) {
            console.log(error);
        }
    }
}

