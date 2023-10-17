import { observer } from 'mobx-react-lite';
import { Segment, Grid, Icon } from 'semantic-ui-react'
import { Activity } from "../../../app/models/activity";
import { useStore } from '../../../app/stores/store';

interface Props {
    activity: Activity
}


export default observer(function ActivityDetailedInfo({ activity }: Props) {
    const {userStore} = useStore();   

    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p>Amount if you win: {activity.amountifwon}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p> Mandatory: {activity.ismandatoryactivity}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                    <p>Amount if you loose: {activity.amountiflose}</p>
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