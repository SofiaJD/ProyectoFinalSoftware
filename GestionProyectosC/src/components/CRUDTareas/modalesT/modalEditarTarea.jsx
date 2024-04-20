import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalEditarTarea = ({ idTarea, fetchTareas }) => {

  const [nombre, setNombre] = useState('');
    const [descripcion, setDescripcion] = useState('');
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');
    const [estado, setEstado] = useState('');
    const [proyectoID, setProyectoID] = useState('');
  const [registroExitoso, setRegistroExitoso] = useState(false);

  useEffect(() => {
    const fetchTareaData = async () => {
      if (idTarea) {
        try {
          const response = await api.get(`/Tareas/${idTarea}`);
          const tarea = await response.data;

          const formattedFechaInicio = new Date(tarea.fechaInicio).toISOString().slice(0, 10);
          const formattedFechaFin = new Date(tarea.fechaFin).toISOString().slice(0, 10);

          setNombre(tarea.nombre);
          setDescripcion(tarea.descripcion);
          setFechaInicio(formattedFechaInicio);
        setFechaFin(formattedFechaFin);
          // setFechaInicio(tarea.fechaInicio);
          // setFechaFin(tarea.fechaFin);
          setEstado(tarea.estado);
          setProyectoID(tarea.proyectoID);
        } catch (error) {
          console.error(error);
        }
      }
    };
    fetchTareaData();
  }, [idTarea]);

  const handleEditarTarea = async (e) => {
    e.preventDefault();

    const data = {
      id: idTarea,
      nombre,
      descripcion,
      fechaInicio,
      fechaFin,
      estado, 
      proyectoID,
    };

    try {
      const response = await api.put('/Tareas/' + idTarea, data);
      console.log(response);
      setRegistroExitoso(true);
      fetchTareas();
      const timeoutId = setTimeout(() => {
        handleCerrarModal();
      }, 1000);
      return () => clearTimeout(timeoutId);
    } catch (error) {
      console.error(error);
    }
  };

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalEditarTarea');
    modal.close();
  };

  return (
    <dialog id="modalEditarTarea">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Editar Tarea</h2>
        <p>Complete los campos a continuación para editar la información de la Tarea.</p>
        <form action="" id="editarTarea" onSubmit={handleEditarTarea}>
          <label htmlFor="id">
            ID
            <input type="text" id="id" value={idTarea} disabled />
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
            <label htmlFor="proyectoID">
              Proyecto
              <input
                type="number"
                id="proyectoID"
                value={parseInt(proyectoID)}
                onChange={(e) => setClienteID(e.target.value)}
                required
              />
            </label>
          {registroExitoso && (
            <div id="successMessage">
              <p>¡Actualización de Tarea exitosa!</p>
            </div>
          )}
          <button type="submit">Guardar</button>
        </form>
      </div>
    </dialog>
  );
};

export default ModalEditarTarea;

