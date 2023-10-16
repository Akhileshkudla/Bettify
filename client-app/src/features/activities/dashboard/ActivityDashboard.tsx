import { Grid } from 'semantic-ui-react';
import ActivityList from './ActivityList';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import ActivityFilters from './ActivityFilters';
import ActivityAmount from './ActivityAmount';

export default observer( function ActvityDashboard() {
    const {activityStore, userStore : {getallusers}} = useStore();
    const { loadActivities, activityRegistry } = activityStore;

    useEffect(() => {
        if(activityRegistry.size <= 1) loadActivities();
        getallusers();
    }, [activityRegistry.size, loadActivities, getallusers])  
  
    if(activityStore.loadingInitial) return <LoadingComponent content='Loading activities...' />

    return(
        <Grid>
            <Grid.Column width={'10'}>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityAmount />
                <ActivityFilters />
            </Grid.Column>
        </Grid>
    )
})