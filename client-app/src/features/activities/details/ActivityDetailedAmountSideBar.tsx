import { observer } from 'mobx-react-lite';
import { Segment, Grid, Icon } from 'semantic-ui-react'
import { Activity } from "../../../app/models/activity";

interface Props {
    activity: Activity
}


export default observer(function ActivityDetailedInfo({ activity }: Props) {
    
    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='inr' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        <p>Amount if you win: {activity.amountIfWon}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='inr' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                    <p>Amount if you loose: {activity.amountIfLose}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            {activity.isMandatoryActivity && <>
            <Segment attached >
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='warning sign' />
                    </Grid.Column>
                    <Grid.Column width={10}>
                        
                         <p>This is Mandatory activity</p>
                                          
                    </Grid.Column>
                </Grid>
            </Segment>            
            <Segment>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='info' size='large' color='orange' />
                    </Grid.Column>
                    <Grid.Column width={12}>
                        <p>Since this is a mandatory activity, If bet isn't placed before the closing time, extra fees may apply.</p>
                    </Grid.Column>
                </Grid>
            </Segment></>
            }
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