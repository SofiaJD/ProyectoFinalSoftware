import React, { useState, useEffect } from 'react';
import './crudClientes.css';
import api from '../../api';
import ModalAgregarCliente from './modalesC/modalAgregarCliente';
import ModalEditarCliente from './modalesC/modalEditarCliente';
import ModalMostrarCliente from './modalesC/modalMostrarCliente';

const CRUDClientes = () => {
  const [clientes, setClientes] = useState([]);
  const [clientesFiltrados, setClientesFiltrados] = useState([]); 
  const [idClienteSeleccionado, setIdClienteSeleccionado] = useState(null);

  const fetchClientes = async () => {
    try {
      const response = await api.get('/Clientes');
      setClientes(response.data);
      setClientesFiltrados(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchClientes();
  }, []);

  const handleSearch = (event) => {
    const searchTerm = event.target.value.toLowerCase();
    const filteredClientes = clientes.filter((cliente) =>
      cliente.nombre.toLowerCase().includes(searchTerm) ||
      cliente.contacto.toLowerCase().includes(searchTerm) ||
      cliente.email.toLowerCase().includes(searchTerm)
    );
    setClientesFiltrados(filteredClientes);
  };

  const handleAbrirModalAgregar = () => {
    const modal = document.getElementById('modalAgregarCliente');
    modal.showModal();
  };

  const handleAbrirModalEditar = (cliente) => {
    const modal = document.getElementById('modalEditarCliente');
    modal.showModal();
    setIdClienteSeleccionado(cliente.id);
  };

  const handleAbrirModalMostrar = (idCliente) => {
    setIdClienteSeleccionado(idCliente);
    const modal = document.getElementById('modalMostrarCliente');
    modal.showModal();
  };

  const handleEliminarCliente = async (idCliente) => {
    try {
      const response = await api.delete(`/Clientes/${idCliente}`);
      console.log('Cliente eliminado:', response);
      fetchClientes();
    } catch (error) {
      console.error('Error al eliminar cliente:', error);
    }
  };

  return (
    <div id="containerClientes">
      <h1>Gestión de clientes</h1>
      <div id="controles">
        <button type="button" id="btnNuevoCliente" onClick={handleAbrirModalAgregar}>
          <span className="material-symbols-outlined">add</span>
          <p>Agregar nuevo cliente</p>
        </button>
        <div id="buscador">
          <label htmlFor="buscar">
            <span className="material-symbols-outlined">search</span>
          </label>
          <input type="search" placeholder="Buscar" id="buscar" onChange={handleSearch} />
        </div>
      </div>
      <p>Listado de consultores registrados:</p>
      <div id="tabla">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre completo</th>
              <th>Contacto</th>
              <th>Correo eléctronico</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {clientesFiltrados.map((cliente) => (
              <tr key={cliente.id}>
                <td>{cliente.id}</td>
                <td>{cliente.nombre}</td>
                <td>{cliente.contacto}</td>
                <td>{cliente.email}</td>
                <td className="acciones">
                  <button onClick={() => handleAbrirModalMostrar(cliente.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(cliente)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarCliente(cliente.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <ModalAgregarCliente fetchClientes={fetchClientes} />
      <ModalEditarCliente idCliente={idClienteSeleccionado} fetchClientes={fetchClientes} />
      <ModalMostrarCliente idCliente={idClienteSeleccionado} />
    </div>
  );
};

export default CRUDClientes;