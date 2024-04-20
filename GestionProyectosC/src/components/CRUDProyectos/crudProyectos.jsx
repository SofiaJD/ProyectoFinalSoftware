import React, { useState, useEffect } from 'react';
import './crudProyectos.css';
import api from '../../api';
import ModalAgregarProyecto from './modalesP/modalAgregarProyecto';
import ModalEditarProyecto from './modalesP/modalEditarProyecto';
import ModalMostrarProyecto from './modalesP/modalMostrarProyecto';
import ModalAgregarAsignacion from '../CRUDAsignaciones/modalesA/modalAgregarAsignacion'

const CRUDProyectos = () => {
  const [proyectos, setProyectos] = useState([]);
  const [proyectosFiltrados, setProyectosFiltrados] = useState([]);
  const [idProyectoSeleccionado, setIdProyectoSeleccionado] = useState(null);

  const fetchProyectos = async () => {
    try {
      const response = await api.get('/Proyecto');
      setProyectos(response.data);
      setProyectosFiltrados(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchProyectos();
  }, []);

  const handleSearch = (event) => {
    const searchTerm = event.target.value.toLowerCase();
    const filteredProyectos = proyectos.filter((proyecto) =>
      proyecto.nombre.toLowerCase().includes(searchTerm) ||
      proyecto.nombreCliente.toLowerCase().includes(searchTerm) ||
      proyecto.estado.toLowerCase().includes(searchTerm)
    );
    setProyectosFiltrados(filteredProyectos);
  };

  const handleAbrirModalAgregar = () => {
    const modal = document.getElementById('modalAgregarProyecto');
    modal.showModal();
  };

  const handleAbrirModalEditar = (proyecto) => {
    const modal = document.getElementById('modalEditarProyecto');
    modal.showModal();
    setIdProyectoSeleccionado(proyecto.id);
  };

  const handleAbrirModalMostrar = (idProyecto) => {
    setIdProyectoSeleccionado(idProyecto);
    const modal = document.getElementById('modalMostrarProyecto');
    modal.showModal();
  };

  const handleAbrirModalAgregarAsignacion = () => {
    const modal = document.getElementById('modalAgregarAsignacion');
    modal.showModal();
};

  const handleEliminarProyecto = async (idProyecto) => {
    try {
      const response = await api.delete(`/Proyecto/${idProyecto}`);
      console.log('Proyecto eliminado:', response);
      fetchProyectos();
    } catch (error) {
      console.error('Error al eliminar proyecto:', error);
    }
  };

  return (
    <div id="containerProyectos">
      <h1>Gestión de proyectos</h1>
      <div id="controles">
        <button type="button" id="btnNuevoProyecto" onClick={handleAbrirModalAgregar}>
          <span className="material-symbols-outlined">add</span>
          <p>Agregar nuevo Proyecto</p>
        </button>
        <div id="buscador">
          <label htmlFor="buscar">
            <span className="material-symbols-outlined">search</span>
          </label>
          <input type="search" placeholder="Buscar" id="buscar" onChange={handleSearch} />
        </div>
      </div>
      <p>Listado de proyectos registrados:</p>
      <div id="tabla">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Cliente</th>
              <th>Estado</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {proyectosFiltrados.map((proyecto) => (
              <tr key={proyecto.id}>
                <td>{proyecto.id}</td>
                <td>{proyecto.nombre}</td>
                <td>{proyecto.nombreCliente}</td>
                <td>{proyecto.estado}</td>
                <td className="acciones">
                  <div id="acciones1">
                  <button onClick={() => handleAbrirModalMostrar(proyecto.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(proyecto)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarProyecto(proyecto.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                  </div>
                  <button type="button" onClick={() => handleAbrirModalAgregarAsignacion(proyecto.id)}>Agregar asignación</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <ModalAgregarProyecto fetchProyectos={fetchProyectos} />
      <ModalEditarProyecto idProyecto={idProyectoSeleccionado} fetchProyectos={fetchProyectos} />
      <ModalMostrarProyecto idProyecto={idProyectoSeleccionado} />
      <ModalAgregarAsignacion/>
    </div>
  );
};

export default CRUDProyectos;