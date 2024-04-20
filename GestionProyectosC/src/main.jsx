import React from 'react'
import ReactDOM from 'react-dom/client'
// import App from './Views/App'

import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from './views/App'
import Registro from "./views/Registro";
import Login from './views/Login';
import Inicio from './views/Inicio';
import Clientes from './views/Clientes'
import Consultores from './views/Consultores';
import Proyectos from './views/Proyectos';
import Tareas from './views/Tareas';
import Asignaciones from './views/Asignaciones';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App/>,
  },
  {
    path: "/Login",
    element: <Login/>,
  },
  {
    path: "/Registro",
    element: <Registro/>,
  },
  {
    path: "/Inicio",
    element: <Inicio/>,
  },
  {
    path: "/Clientes",
    element: <Clientes/>,
  },
  {
    path: "/Consultores",
    element: <Consultores/>,
  },
  {
    path: "/Proyectos",
    element: <Proyectos/>,
  },
  {
    path: "/Tareas",
    element: <Tareas/>,
  },
  {
    path: "/Asignaciones",
    element: <Asignaciones/>,
  },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <App/> */}
    <RouterProvider router={router} />
  </React.StrictMode>,
)