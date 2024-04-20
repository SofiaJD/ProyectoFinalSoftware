import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalMostrarCliente = ({ idCliente }) => {
  const [cliente, setCliente] = useState(null);

  useEffect(() => {
    const fetchClienteData = async () => {
      try {
        const response = await api.get(`/Clientes/${idCliente}`);
        const clienteData = await response.data;

        setCliente(clienteData);
      } catch (error) {
        console.error(error);
      }
    };
    if (idCliente) {
      fetchClienteData();
    }
  }, [idCliente]);

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalMostrarCliente');
    modal.close();
  };

  return (
    <dialog id="modalMostrarCliente">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Ver datos de cliente</h2>
        <p>Visualice todos los datos del cliente</p>
        {cliente && (
          <div id="containerInfo">
            <p><strong>ID</strong> <br/> {cliente.id}</p>
            <p><strong>Nombre completo</strong> <br/> {cliente.nombre}</p>
            <p><strong>Contacto</strong> <br/> {cliente.contacto}</p>
            <p><strong>Correo electrónico</strong> <br/> {cliente.email}</p>
            <p><strong>Teléfono</strong> <br/> {cliente.telefono}</p>
          </div>
        )}
      </div>
    </dialog>
  );
};

export default ModalMostrarCliente;

