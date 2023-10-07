import { Fragment, useEffect, useState } from 'react'
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActvityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios.get<Activity[]>('http://localhost:5000/api/activities')
      .then(response => {
        setActivities(response.data)
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
    activity.id 
    ? setActivities([...activities.filter(x => x.id !== activity.id), activity])
    : setActivities([...activities, {...activity, id:uuid()}])
    setEditMode(false);
    setSelectedActivity(activity);
  }

  function HandleDeleteActivity(id: string){
    setActivities([...activities.filter(x => x.id !== id)])
  }

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
        />
      </Container>
    </Fragment>    
  )
}

export default App
