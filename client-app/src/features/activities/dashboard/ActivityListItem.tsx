import { Button, Icon, Item, Label, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { Link } from "react-router-dom";
import { format } from "date-fns";
import ActivityListItemAttendee from "./ActivityListItemAttendee";

interface Props{
    activity: Activity
}

export default function ActivityListItem({activity} : Props){

    return(
        <Segment.Group>
            <Segment>
                {activity.isCancelled && 
                    <Label attached="top" color="red" content='Completed' style={{textAlign: 'center'}} />
                }
                {activity.isMandatoryActivity &&
                    <Label style={{position : 'absolute', zIndex: 1000, right: -14, top: 20}} tag color='teal' content='Mandatory' />
                }
                <Item.Group>
                    <Item>
                        <Item.Image style={{marginBottom: 5}} size='tiny' circular src='/assets/cup.png' />
                        <Item.Content>
                            <Item.Header as={Link} to={`/activities/${activity.id}`}>
                                {activity.title}
                            </Item.Header>
                            <Item.Description>Hosted by <Link to={`/profile/${activity.hostUsername}`}>{activity.host?.displayName}</Link></Item.Description>
                            {activity.isHost && (
                                <Item.Description>
                                    <Label basic color="orange">
                                        You are hosting this event.
                                    </Label>
                                </Item.Description>
                            )}
                            {!activity.isHost && activity.isGoing && (
                                <Item.Description>
                                    <Label basic color="green">
                                        You are participating in this event.
                                    </Label>
                                </Item.Description>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock' /> {format(activity.date!, 'dd MMM yyyy h:mm aa')}
                    <Icon name='marker' /> {activity.venue}
                </span>
            </Segment>
            <Segment secondary>
                <ActivityListItemAttendee attendees={activity.attendees!}/>
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button animated as={Link} to={`/activities/${activity.id}`} color="teal" floated="right" content='View' >
                <Button.Content visible>Next</Button.Content>
                <Button.Content hidden>
                    <Icon name='eye' />
                </Button.Content>
                </Button>
            </Segment>
        </Segment.Group>
    )
}