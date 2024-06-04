import React from 'react';
import './App.css';
import CoachesPage from './pages/Coachespage';
import CompetitionsPage from './pages/Competitionspage';
import HomePage from './pages/Homepage';
import {createBrowserRouter,RouterProvider} from "react-router-dom";

const router = createBrowserRouter([
  {
    path:"/",
    element: <HomePage /> 
  },
  {
    path:"/coachespage",
    element: <CoachesPage/> 
  },
  {
    path:"/competitions",
    element: <CompetitionsPage/> 
  },
]);

function App() {
  
  return (
    <>
    <RouterProvider router={router} />
    </>

  )
}

export default App
