import { Fragment, useEffect, useState } from 'react'
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActvityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';
import agent from '../api/agent';
import LoadingComponent from './LoadingComponent';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    agent.Activities.list()
      .then(response => {
        let activities: Activity[] = [];
        response.forEach(act =>{
          act.date =act.date.split('T')[0];
          activities.push(act);
        })

        setActivities(activities)
        setLoading(false);
      })
  }, [])

  function HandleSelectedActivity(id: string){
    setSelectedActivity(activities.find(x => x.id === id))    
  }

  function HandleCancelSelectActivity()  {    
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string){
    id ? HandleSelectedActivity(id) : HandleCancelSelectActivity();
    setEditMode(true);
  }

  function handleFormClose(){
    setEditMode(false);
  }

  function HandleCreateOrEditActivity(activity : Activity){
    setSubmitting(true);
    if(activity.id){
      agent.Activities.update(activity).then(() =>{
        setActivities([...activities.filter(x => x.id !== activity.id), activity])
        setSelectedActivity(activity);
        setEditMode(false);
        setSubmitting(false);
      })
    }
    else{
      activity.id = uuid();
      agent.Activities.create(activity).then(() => {
        setActivities([...activities, activity]);
        setSelectedActivity(activity);
        setEditMode(false);
        setSubmitting(false);
      })            
    }
  }

  function HandleDeleteActivity(id: string){
    setSubmitting(true);
    agent.Activities.delete(id).then(() =>{
      setActivities([...activities.filter(x => x.id !== id)])
      setSubmitting(false);
    })
    
  }

  if(loading) return <LoadingComponent content='Loading app' />

  return (
    <Fragment>
      <NavBar openForm={handleFormOpen} />
      <Container style={{marginTop: '7em'}}>
        <ActvityDashboard 
          activities={activities} 
          selectedActivity={selectedActivity}
          selectActivity={HandleSelectedActivity}
          cancelSelectActivity={HandleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={HandleCreateOrEditActivity}
          deleteActivity={HandleDeleteActivity}
          submitting={submitting}
        />
      </Container>
    </Fragment>    
  )
}

export default App
