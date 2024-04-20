import React, {useState, useEffect} from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import Separador from '../../assets/Separador.svg';
import './menu.css';
import Login from '../../views/Login';

const Menu = ( {children} ) => {

    const navigate = useNavigate();

    const handleLogout = () => {
        navigate('/Login');
    };

    const [activeLink, setActiveLink] = useState('');
    const location = useLocation();

    useEffect(() => {
        const currentPath = location.pathname;
        setActiveLink(currentPath);
    }, [location]);

  return (
    <div id="containerPrincipal">
        <nav id="barra">
            
            {/* <img src={Separador}></img> */}
            {/* <div id="usuario">
                <p>Nombre completo</p>
            </div> */}
        </nav>
        <aside id="menuLateral">
            <Link to="/Inicio" className={`opcion ${activeLink === '/Inicio' ? 'active' : ''}`} id="Inicio">
                <span className="material-symbols-outlined">
                    home
                    </span>
                <p>Inicio</p>
            </Link>
            <Link to="/Clientes" className={`opcion ${activeLink === '/Clientes' ? 'active' : ''}`} id="Clientes">
                <span className="material-symbols-outlined">
                    group
                </span>
                <p>Clientes</p>
            </Link>
            <Link to="/Consultores" className={`opcion ${activeLink === '/Consultores' ? 'active' : ''}`} id="Consultores">
                <span className="material-symbols-outlined">
                    support_agent
                </span>
                <p>Consultores</p>
            </Link>
            <Link to="/Proyectos" className={`opcion ${activeLink === '/Proyectos' ? 'active' : ''}`} id="Proyectos">
                <span className="material-symbols-outlined">
                    work
                    </span>
                    <p>Proyectos</p>
            </Link>
            <Link to="/Tareas" className={`opcion ${activeLink === '/Tareas' ? 'active' : ''}`} id="Tareas">
                <span className="material-symbols-outlined">
                    task_alt
                    </span>
                    <p>Tareas</p>
            </Link>
            <hr></hr>
            <Link to="" className="opcion" id="cerrarSesion" onClick={handleLogout}>
                <span className="material-symbols-outlined">
                    logout
                </span>
                <p>Cerrar sesión</p>
            </Link>
        </aside>
        <div id="containerCentral">
            {children}
        </div>
    </div>
  );
};

export default Menu;