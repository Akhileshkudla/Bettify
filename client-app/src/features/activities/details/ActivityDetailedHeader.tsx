import { observer } from 'mobx-react-lite';
import {Button, Header, Item, Segment, Image, Label, Icon, Popup} from 'semantic-ui-react'
import {Activity} from "../../../app/models/activity";
import { Link } from 'react-router-dom';
import { format } from 'date-fns';
import { useStore } from '../../../app/stores/store';
import { useEffect, useState } from 'react';
import ActivityDetailsPlaceBet from './ActivityDetailsPlaceBet';
import ActivityDetailsWinningBet from './ActivityDetailsWinningBet';

const activityImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    activity: Activity
}

export default observer (function ActivityDetailedHeader({activity}: Props) {
    const {activityStore : {updateAttendance, cancelActivityToggle}} = useStore()
    const [isButtonDisabled, setIsButtonDisabled] = useState(false);
    const { modalStore} = useStore();

    useEffect(() => {
        const currentTime = new Date();
    
        // Compare current time with activity date
        if (currentTime > activity.date! || activity.isCancelled) {
          setIsButtonDisabled(true);
        } else {
          setIsButtonDisabled(false);
        }
      }, [activity.date, activity.isCancelled]);

      
    return (
        <Segment.Group>
            <Segment basic attached='top' style={{padding: '0'}}>
                {activity.isCancelled &&
                    <Label style={{position : 'absolute', zIndex: 1000, left: -14, top: 20}} ribbon color='red' content='Completed' />
                }
                {activity.isMandatoryActivity &&
                    <Label style={{position : 'absolute', zIndex: 1000, left: -14, top: 60}} ribbon color='purple' content='Mandatory' />
                }
                <Image src={`/assets/categoryImages/${activity.category.toLowerCase()}.jpg`} fluid style={activityImageStyle}/>
                <Segment style={activityImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={activity.title}
                                    style={{color: 'white'}}
                                />
                                <p>{format (activity.date!, 'dd MMM yyyy')}</p>
                                <p>
                                    Hosted by <strong><Link to={`/profile/${activity.host?.username}`}>{activity.host?.displayName}</Link></strong>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Popup
                trigger={
                <Segment clearing attached='bottom'>
                    {activity.isHost ? (
                        <>
                        
                                <Button 
                                    color={activity.isCancelled ? "green" : 'red'}
                                    floated='left'
                                    basic
                                    content={activity.isCancelled ? 'Re-activate event' :'Complete event'}
                                    onClick={ () => {
                                        modalStore.openModal(<ActivityDetailsWinningBet 
                                                                            activitiesOptions={activity.options} 
                                                                        />)
                                        {cancelActivityToggle}
                                    }}
                                />
                            <Button as={Link} to={`/manage/${activity.id}`} color='orange' floated='right' >
                                Manage event
                            </Button>
                        </>
                        
                    ) : activity.isGoing ? (
                        
                                <Button disabled={isButtonDisabled} onClick={() => updateAttendance("")}>Clear selection</Button>
                            
                    ) : (
                        <Button animated disabled={isButtonDisabled} onClick={ () => modalStore.openModal(<ActivityDetailsPlaceBet activity={activity}/>)} color='facebook'>
                            <Button.Content visible>Place bet</Button.Content>
                            <Button.Content hidden>
                                <Icon name='arrow right' />
                            </Button.Content>
                        </Button>                    
                    )}                
                </Segment>
            }
            content={ 'Voting will be closed by ' + format(activity.date!, 'dd MMM yyyy h:mm aa')}
            basic
        />
        </Segment.Group>
    )
})