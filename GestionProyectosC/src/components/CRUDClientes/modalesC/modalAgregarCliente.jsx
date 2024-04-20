import React, {useState} from 'react';
import './modal.css';
import api from '../../../api';

const ModalAgregarCliente = ({fetchClientes}) => {

    const [nombre, setNombre] = useState('');
    const [contacto, setContacto] = useState('');
    const [email, setEmail] = useState('');
    const [telefono, setTelefono] = useState('');
    const [registroExitoso, setRegistroExitoso] = useState(false);

    const handleAgregarCliente = async (e) => {
        e.preventDefault();

        const data = {
        nombre,
        contacto,
        email,
        telefono,
        };

        try {
        const response = await api.post('/Clientes/Crear Clientes', data);
        console.log(response);
        setRegistroExitoso(true);
        fetchClientes();
        const timeoutId = setTimeout(() => {
            handleCerrarModal();
        }, 1000);
        setNombre('');
        setContacto('');
        setEmail('');
        setTelefono('');
        return () => clearTimeout(timeoutId);
        } catch (error) {
        console.error(error);
        }
    };

    const handleCerrarModal = () => {
        const modal = document.getElementById('modalAgregarCliente');
        modal.close();
      };

  return (
        <dialog id="modalAgregarCliente">
            <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
            <span className="material-symbols-outlined btnCerrar">
                close
            </span>
        </button>
        <div id="container">
          <h2>Agregar nuevo cliente</h2>
          <p>Complete los campos a continuación para registrar un nuevo cliente.</p>
          <form action="" id="registrarCliente" onSubmit={handleAgregarCliente}>
            <label htmlFor="nombre">
              Nombre completo
              <input
                type="text"
                id="nombre"
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
                required
              />
            </label>
            <label htmlFor="contacto">
              Contacto
              <input
                type="text"
                id="contacto"
                value={contacto}
                onChange={(e) => setContacto(e.target.value)}
                required
              />
            </label>
            <label htmlFor="email">
              Correo electrónico
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </label>
            <label htmlFor="telefono">
              Teléfono
              <input
                type="tel"
                id="telefono"
                value={telefono}
                onChange={(e) => setTelefono(e.target.value)}
                required
              />
            </label>
            {registroExitoso && (
            <div id="successMessage">
              <p>¡Registro de nuevo cliente exitoso!</p>
            </div>
          )}
          </form>
          <button type="submit" form="registrarCliente">
              Guardar
            </button>
        </div>
        </dialog>
  );
};

export default ModalAgregarCliente;