import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityDetails from "../../features/activities/details/ActivityDetails";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import TestErrors from "../../features/errors/TestError";
import ProfilePage from "../../features/profiles/ProfilePage";
import App from "../layout/App";
import RequireAuth from "./RequireAuth";
import UsersForm from "../../features/users/UsersForm";
import ChangePassword from "../layout/ChangePassword";
import AcitivityForm from "../../features/activities/form/AcitivityForm";
import TransactionForm from "../../features/users/TransactionForm";
import Leaderboard from "../../features/users/Leaderboard";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [            
            {element: <RequireAuth />, children: [
                {path: 'activities', element: <ActivityDashboard />},
                {path: 'activities/:id', element: <ActivityDetails />},
                {path: 'createActivity', element: <AcitivityForm key='create' />},
                {path: 'manage/:id', element: <AcitivityForm key='manage' />},
                {path: 'profile/:username', element: <ProfilePage />},
                {path: 'errors', element: <TestErrors />},              
                {path: 'users', element: <UsersForm />},
                {path: 'leaderboard', element: <Leaderboard />},
                {path: 'transcations', element: <TransactionForm />},
                {path: 'changepassword', element: <ChangePassword />},
            ]},
            {path: 'not-found', element: <NotFound />},
            {path: 'server-error', element: <ServerError />},
            {path: '*', element: <Navigate replace to='/not-found' />},
        ]
    }
]

export const router = createBrowserRouter(routes);