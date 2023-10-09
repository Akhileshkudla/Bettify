import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import AcitivityForm from "../../features/activities/form/AcitivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [            
            {path: 'activities', element: <ActivityDashboard />},
            {path: 'activities/:id', element: <ActivityDetails />},
            {path: 'createActivity', element: <AcitivityForm key='create' />},
            {path: 'manage/:id', element: <AcitivityForm key={'manage'} />},
        ]
    }
]

export const router = createBrowserRouter(routes)