import React from 'react';
import { Link } from 'react-router-dom';
import './navbar.css'

const Navbar = () => {
  return (
    <nav>
      <Link to="/" id='inicio'>Gestión PHC</Link>
      <div>
        <Link to="/Login" id="iniciarSesion">Iniciar sesión</Link>
        <Link to="/Registro" id="registrarse">Registrarse</Link>
      </div>
    </nav>
  );
};

export default Navbar;