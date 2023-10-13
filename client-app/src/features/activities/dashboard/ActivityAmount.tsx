import { observer } from 'mobx-react-lite';
import { Segment, Grid, Icon } from 'semantic-ui-react'
import { useStore } from '../../../app/stores/store';


export default observer(function ActivityAmount() {
    const {userStore} = useStore();

    return (
        <Segment.Group style={{width: '100%', marginTop: 30 }}>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='bitcoin' />
                    </Grid.Column>
                    <Grid.Column width={8}>
                        <p>Amount you owe: </p>
                    </Grid.Column>
                    <Grid.Column width={1}>
                        <p> {userStore.user?.username}</p>
                    </Grid.Column>
                </Grid>
            </Segment>            
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='bitcoin' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={8}>
                        <p>Total amount collected: </p>
                    </Grid.Column>
                    <Grid.Column width={1}>
                        <p>{userStore.user?.username}</p>
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