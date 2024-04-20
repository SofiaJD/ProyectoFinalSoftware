import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalEditarProyecto = ({ idProyecto, fetchProyectos }) => {

  const [nombre, setNombre] = useState('');
    const [descripcion, setDescripcion] = useState('');
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');
    const [estado, setEstado] = useState('');
    // const [nombreCliente, setNombreCliente] = useState('');
    const [clienteID, setClienteID] = useState('');
  const [registroExitoso, setRegistroExitoso] = useState(false);

  useEffect(() => {
    const fetchProyectoData = async () => {
      if (idProyecto) {
        try {
          const response = await api.get(`/Proyecto/${idProyecto}`);
          const proyecto = await response.data;

          const formattedFechaInicio = new Date(proyecto.fechaInicio).toISOString().slice(0, 10);
          const formattedFechaFin = new Date(proyecto.fechaFin).toISOString().slice(0, 10);

          setNombre(proyecto.nombre);
          setDescripcion(proyecto.descripcion);
          setFechaInicio(formattedFechaInicio);
        setFechaFin(formattedFechaFin);
          setEstado(proyecto.estado);
          setClienteID(proyecto.clienteID);
        } catch (error) {
          console.error(error);
        }
      }
    };
    fetchProyectoData();
  }, [idProyecto]);

  const handleEditarProyecto = async (e) => {
    e.preventDefault();

    const data = {
      id: idProyecto,
      nombre,
      descripcion,
      fechaInicio,
      fechaFin,
      estado, 

      
      clienteID,
    };

    try {
      const response = await api.put('/Proyecto/' + idProyecto, data);
      console.log(response);
      setRegistroExitoso(true);
      fetchProyectos();
      const timeoutId = setTimeout(() => {
        handleCerrarModal();
      }, 1000);
      return () => clearTimeout(timeoutId);
    } catch (error) {
      console.error(error);
    }
  };

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalEditarProyecto');
    modal.close();
  };

  return (
    <dialog id="modalEditarProyecto">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Editar Proyecto</h2>
        <p>Complete los campos a continuación para editar la información del Proyecto.</p>
        <form action="" id="editarProyecto" onSubmit={handleEditarProyecto}>
          <label htmlFor="id">
            ID
            <input type="text" id="id" value={idProyecto} disabled />
          </label>
          <label htmlFor="nombre">
            Nombre
            <input
              type="text"
              id="nombre"
              value={nombre}
              onChange={(e) => setNombre(e.target.value)}
              required
            />
          </label>
          <label htmlFor="descripcion">
              Descripción
              <textarea id="descripcion" value={descripcion} onChange={(e) => setDescripcion(e.target.value)} 
              cols="30" rows="5"></textarea>
            </label>
            <label htmlFor="fechaInicio">
              Fecha de inicio
              <input
                type="date"
                id="fechaInicio"
                value={fechaInicio}
                onChange={(e) => setFechaInicio(e.target.value)}
                required
              />
            </label>
            <label htmlFor="fechaFin">
              Fecha de finalización
              <input
                type="date"
                id="fechaFin"
                value={fechaFin}
                onChange={(e) => setFechaFin(e.target.value)}
                required
              />
              </label>
              <label htmlFor="estado">
              Estado
              <input
                type="text"
                id="estado"
                value={estado}
                onChange={(e) => setEstado(e.target.value)}
                required
              />
            </label>
            <label htmlFor="clienteID">
              Cliente
              <input
                type="number"
                id="clienteID"
                value={clienteID}
                onChange={(e) => setClienteID(e.target.value)}
                required
              />
            </label>
          {registroExitoso && (
            <div id="successMessage">
              <p>¡Actualización de Proyecto exitosa!</p>
            </div>
          )}
          <button type="submit">Guardar</button>
        </form>
      </div>
    </dialog>
  );
};

export default ModalEditarProyecto;

