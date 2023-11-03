import { Grid, Loader, Image } from 'semantic-ui-react';
import ActivityList from './ActivityList';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react';
import ActivityFilters from './ActivityFilters';
import ActivityAmount from './ActivityAmount';
import { PagingParams } from '../../../app/models/pagination';
import ActivityListItemPlaceholder from './ActivityListItemPlaceHolder';
import InfiniteScroll from 'react-infinite-scroller';

export default observer( function ActvityDashboard() {
    const {activityStore, userStore : {getallusers}} = useStore();
    const { loadActivities, setPagingParams, pagination } = activityStore;
    const [loadingNext, setLoadingNext] = useState(false);

    function handleGetNext() {
        setLoadingNext(true);
        setPagingParams(new PagingParams(pagination!.currentPage + 1));
        loadActivities().then(() => setLoadingNext(false));
    }
    useEffect(() => {
        loadActivities();
        getallusers();
    }, [loadActivities, getallusers])  
  

    return(
        <Grid>
            <Grid.Column width={'10'}>
            <Image  alt='Data' src={'https://res.cloudinary.com/dtpzeuru1/image/upload/v1698993912/xscr5bsiesogulxebfww.jpg'} />
                {activityStore.loadingInitial && !loadingNext ? (
                    <>
                        <ActivityListItemPlaceholder />
                        <ActivityListItemPlaceholder />
                    </>
                ) : (
                        <InfiniteScroll
                            pageStart={0}
                            loadMore={handleGetNext}
                            hasMore={!loadingNext && !!pagination && pagination.currentPage < pagination.totalPages}
                            initialLoad={false}
                        >
                            <ActivityList />
                        </InfiniteScroll>
                    )}
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityAmount />
                <ActivityFilters />
            </Grid.Column>
            <Grid.Column width='10'>
                <Loader active={loadingNext} />
            </Grid.Column>
        </Grid>
    )
})