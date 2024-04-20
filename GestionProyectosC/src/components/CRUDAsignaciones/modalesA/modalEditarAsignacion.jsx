import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalEditarCliente = ({ idCliente, fetchClientes }) => {

  const [nombre, setNombre] = useState('');
  const [contacto, setContacto] = useState('');
  const [email, setEmail] = useState('');
  const [telefono, setTelefono] = useState('');
  const [registroExitoso, setRegistroExitoso] = useState(false);

  useEffect(() => {
    const fetchClienteData = async () => {
      if (idCliente) {
        try {
          const response = await api.get(`/Clientes/${idCliente}`);
          const cliente = await response.data;

          setNombre(cliente.nombre);
          setContacto(cliente.contacto);
          setEmail(cliente.email);
          setTelefono(cliente.telefono);
        } catch (error) {
          console.error(error);
        }
      }
    };
    fetchClienteData();
  }, [idCliente]);

  const handleEditarCliente = async (e) => {
    e.preventDefault();

    const data = {
      id: idCliente,
      nombre,
      contacto,
      email,
      telefono,
    };

    try {
      const response = await api.put('/Clientes/' + idCliente, data);
      console.log(response);
      setRegistroExitoso(true);
      fetchClientes();
      const timeoutId = setTimeout(() => {
        handleCerrarModal();
      }, 1000);
      return () => clearTimeout(timeoutId);
    } catch (error) {
      console.error(error);
    }
  };

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalEditarCliente');
    modal.close();
  };

  return (
    <dialog id="modalEditarCliente">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Editar cliente</h2>
        <p>Complete los campos a continuación para editar la información del cliente.</p>
        <form action="" id="editarCliente" onSubmit={handleEditarCliente}>
          <label htmlFor="id">
            ID
            <input type="text" id="id" value={idCliente} disabled />
          </label>
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
              <p>¡Actualización de cliente exitosa!</p>
            </div>
          )}
          <button type="submit">Guardar</button>
        </form>
      </div>
    </dialog>
  );
};

export default ModalEditarCliente;

