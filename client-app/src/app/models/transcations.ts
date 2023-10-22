import { User } from "./user";

export interface Transcations {
    id: string,
    date: Date | null,
    name: string,
    amount: number,
    message: string,
    transactionUser: User | null,
    transactionUserId: string
}
