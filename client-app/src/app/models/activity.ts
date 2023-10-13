import { Profile } from "./profile";

export interface Activity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date | null;
    city: string;
    venue: string;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing?: boolean;
    isHost?: boolean
    attendees?: Profile[]
    host?: Profile;
    options: string[];
    winningOption: string;
    amountifwon: number;
    amountiflose: number;
    ismandatoryactivity: boolean;
}

export class ActivityFormValues
  {
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    date: Date | null = null;
    city: string = '';
    venue: string = '';
    options: string[] = [];
    winningOption: string = '';
    amountifwon: number = 0;
    amountiflose: number =0;
    ismandatoryactivity: boolean = false;

	  constructor(activity?: ActivityFormValues) {
      if (activity) {
        this.id = activity.id;
        this.title = activity.title;
        this.category = activity.category;
        this.description = activity.description;
        this.date = activity.date;
        this.venue = activity.venue;
        this.city = activity.city;
        this.options = activity.options;
        this.winningOption = activity.winningOption;
        this.amountifwon = activity.amountifwon;
        this.amountiflose = activity.amountiflose;
        this.ismandatoryactivity = activity.ismandatoryactivity;
      }
    }

  }

  export class Activity implements Activity {
    constructor(init?: ActivityFormValues) {
      Object.assign(this, init);
    }
  }
