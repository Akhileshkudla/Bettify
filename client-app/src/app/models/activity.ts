import { Profile } from "./profile";

export interface Activity {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date | null;
    city: string;
    venue: string;
    options: string[];
    winningOption: string;
    amountIfWon: number;
    amountIfLose: number;
    isMandatoryActivity: boolean;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing?: boolean;
    isHost?: boolean
    attendees: Profile[]
    host?: Profile;    
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
    amountIfWon: number = 0 ;
    amountIfLose: number = 0;
    isMandatoryActivity: boolean = false;

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
        this.amountIfWon = activity.amountIfWon;
        this.amountIfLose = activity.amountIfLose;
        this.isMandatoryActivity = activity.isMandatoryActivity;
      }
    }

  }

  export class Activity implements Activity {
    
  constructor(init?: ActivityFormValues) {
    if (init) {
      Object.assign(this, init);
    }
  }
  }
