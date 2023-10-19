import { observer } from 'mobx-react-lite';
import { Fragment } from 'react';
import { Header, Segment } from "semantic-ui-react";
import { useStore } from '../../../app/stores/store';
import ActivityListItem from './ActivityListItem';

export default observer(function ActivityList() {
    const { activityStore } = useStore();
    const { groupedActivities } = activityStore;

    return (
        <>
            {groupedActivities && groupedActivities.length === 0 && (
                <>
                 <Segment>
                     <span>Please clear filter or choose an earlier date, as there are no activities planned on or after the selected date.</span>
                 </Segment>
                 </>
            )}
            {groupedActivities.map(([group, activities]) => (
                <Fragment key={group}>
                    <Header sub color='teal'>
                        {group}
                    </Header>
                    {activities && activities.length === 0 && (
                        <Segment>
                            <span>0 items found for the date selected, Please choose a older date.</span>
                        </Segment>
                    )}
                    {activities && activities.map(activity => (
                        <ActivityListItem key={activity.id} activity={activity} />
                    ))}
                    
                </Fragment>
            ))}
        </>

    )
})
