import React, { useState, useEffect } from 'react';
import './crudTareas.css';
import api from '../../api';
import ModalAgregarTarea from './modalesT/modalAgregarTarea';
import ModalEditarTarea from './modalesT/modalEditarTarea';
import ModalMostrarTarea from './modalesT/modalMostrarTarea';

const CRUDTareas = () => {
  const [tareas, setTareas] = useState([]);
  const [tareasFiltrados, setTareasFiltrados] = useState([]);
  const [idTareaSeleccionado, setIdTareaSeleccionado] = useState(null);

  const fetchTareas = async () => {
    try {
      const response = await api.get('/Tareas');
      setTareas(response.data);
      setTareasFiltrados(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchTareas();
  }, []);

  const handleSearch = (event) => {
    const searchTerm = event.target.value.toLowerCase();
    const filteredTareas = tareas.filter((Tarea) =>
      Tarea.nombre.toLowerCase().includes(searchTerm) ||
      Tarea.proyectoNombre.toLowerCase().includes(searchTerm) ||
      Tarea.estado.toLowerCase().includes(searchTerm)
    );
    setTareasFiltrados(filteredTareas);
  };

  const handleAbrirModalAgregar = () => {
    const modal = document.getElementById('modalAgregarTarea');
    modal.showModal();
  };

  const handleAbrirModalEditar = (tarea) => {
    const modal = document.getElementById('modalEditarTarea');
    modal.showModal();
    setIdTareaSeleccionado(tarea.id);
  };

  const handleAbrirModalMostrar = (idTarea) => {
    setIdTareaSeleccionado(idTarea);
    const modal = document.getElementById('modalMostrarTarea');
    modal.showModal();
  };

  const handleEliminarTarea = async (idTarea) => {
    try {
      const response = await api.delete(`/Tareas/${idTarea}`);
      console.log('Tarea eliminado:', response);
      fetchTareas();
    } catch (error) {
      console.error('Error al eliminar Tarea:', error);
    }
  };

  return (
    <div id="containerTareas">
      <h1>Gestión de tareas</h1>
      <div id="controles">
        <button type="button" id="btnNuevaTarea" onClick={handleAbrirModalAgregar}>
          <span className="material-symbols-outlined">add</span>
          <p>Agregar nueva tarea</p>
        </button>
        <div id="buscador">
          <label htmlFor="buscar">
            <span className="material-symbols-outlined">search</span>
          </label>
          <input type="search" placeholder="Buscar" id="buscar" onChange={handleSearch} />
        </div>
      </div>
      <p>Listado de tareas registradas:</p>
      <div id="tabla">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Proyecto</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {tareasFiltrados.map((tarea) => (
              <tr key={tarea.id}>
                <td>{tarea.id}</td>
                <td>{tarea.nombre}</td>
                <td>{tarea.proyectoNombre}</td>
                <td>{tarea.estado}</td>
                <td className="acciones">
                  <button onClick={() => handleAbrirModalMostrar(tarea.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(tarea)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarTarea(tarea.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <ModalAgregarTarea fetchTareas={fetchTareas} />
      <ModalEditarTarea idTarea={idTareaSeleccionado} fetchTareas={fetchTareas} />
      <ModalMostrarTarea idTarea={idTareaSeleccionado} />
    </div>
  );
};

export default CRUDTareas;