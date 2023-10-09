import { Navigate, RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import AcitivityForm from "../../features/activities/form/AcitivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";
import TestErrors from "../../features/errors/TestError";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [            
            {path: 'activities', element: <ActivityDashboard />},
            {path: 'activities/:id', element: <ActivityDetails />},
            {path: 'createActivity', element: <AcitivityForm key='create' />},
            {path: 'manage/:id', element: <AcitivityForm key={'manage'} />},
            {path: 'errors', element: <TestErrors />},
            {path: 'not-found', element: <NotFound />},
            {path: 'server-error', element: <ServerError />},
            {path: '*', element: <Navigate replace to='/not-found' />},
        ]
    }
]

export const router = createBrowserRouter(routes)