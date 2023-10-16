import { observer } from 'mobx-react-lite';
import { Segment, Grid, Icon } from 'semantic-ui-react'
import { Activity, ActivityFormValues } from "../../../app/models/activity";
import { useStore } from '../../../app/stores/store';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import ActivityStore from '../../../app/stores/activityStore';

interface Props {
    activity: Activity
}


export default observer(function ActivityDetailedInfo({ activity }: Props) {
    const {userStore, activityStore : {loadActivity, selectedActivity}} = useStore();   

    const [activity2, setActivity] = useState<ActivityFormValues>(new ActivityFormValues());
    const { id } = useParams();

    useEffect(() => {
        if (id) loadActivity(id).then(activity => setActivity(new ActivityFormValues(activity)));
    }, [id, loadActivity])

    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p>Amount if you win: {activityStore.activity.amountifwon}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p> Mandatory: {selectedActivity?.ismandatoryactivity}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                    <p>Amount if you loose: {selectedActivity?.amountiflose}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='bitcoin' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p>Total amount you owe: {userStore.user?.amount}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached='bottom'>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='info' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={12}>
                        <p>Negative numbers mean you receive money, non-negative numbers mean you pay money. </p>
                    </Grid.Column>
                </Grid>
            </Segment>
        </Segment.Group>
    )
})