import React, { useState, useEffect } from 'react';
import './crudAsignaciones.css';
import api from '../../api';
import ModalAgregarAsignacion from './modalesA/modalAgregarAsignacion';
import ModalEditarAsignacion from './modalesA/modalEditarAsignacion';
import ModalMostrarAsignacion from './modalesA/modalMostrarAsignacion';

const CRUDAsignaciones = () => {
  const [asignaciones, setAsignaciones] = useState([]);
  const [asignacionesFiltrados, setAsignacionesFiltrados] = useState([]); 
  const [idAsignacionSeleccionado, setIdAsignacionSeleccionado] = useState(null);

    const [proyectos, setProyectos] = useState([]);
    const [proyectosFiltrados, setProyectosFiltrados] = useState([]);
    const [idProyectoSeleccionado, setIdProyectoSeleccionado] = useState(null);

    useEffect(() => {
      const fetchProyectos = async () => {
        try {
          const response = await api.get('/Proyecto');
          setProyectos(response.data);
          setProyectosFiltrados(response.data);
        } catch (error) {
          console.error(error);
        }
      };

      fetchProyectos();
    }, []);


  const handleSearch = (event) => {
    const searchTerm = event.target.value.toLowerCase();
    const filteredAsignaciones = asignaciones.filter((asignacion) =>
      asignacion.consultorID.toLowerCase().includes(searchTerm)
      // asignacion.contacto.toLowerCase().includes(searchTerm) ||
      // asignacion.email.toLowerCase().includes(searchTerm)
    );
    setAsignacionesFiltrados(filteredAsignaciones);
  };

  const handleAbrirModalAgregar = () => {
    const modal = document.getElementById('modalAgregarAsignacion');
    modal.showModal();
  };

  const handleAbrirModalEditar = (asignacion) => {
    const modal = document.getElementById('modalEditarAsignacion');
    modal.showModal();
    setIdAsignacionSeleccionado(asignacion.id);
  };

  const handleAbrirModalMostrar = (idAsignacion) => {
    setIdAsignacionSeleccionado(idAsignacion);
    const modal = document.getElementById('modalMostrarAsignacion');
    modal.showModal();
  };

  // const handleEliminarAsignacion = async (idAsignacion) => {
  //   try {
  //     const response = await api.delete(`/Asignacion/${consultorId}/${proyectoId}`);
  //     console.log('Asignacion eliminado:', response);
  //     fetchAsignaciones();
  //   } catch (error) {
  //     console.error('Error al eliminar Asignacion:', error);
  //   }
  // };

  return (
    <div id="containerAsignaciones">
      <h1>Gestión de Asignaciones</h1>
      <div id="controles">
        <button type="button" id="btnNuevoAsignacion" onClick={handleAbrirModalAgregar}>
          <span className="material-symbols-outlined">add</span>
          <p>Agregar nuevo Asignacion</p>
        </button>
        <div id="buscador">
          <label htmlFor="buscar">
            <span className="material-symbols-outlined">search</span>
          </label>
          <input type="search" placeholder="Buscar" id="buscar" onChange={handleSearch} />
        </div>
      </div>
      <p>Listado de asignaciones registrados:</p>
      <div id="tabla">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre del proyecto</th>
              <th>Consultor asignado</th>
              <th>Fecha asignacion</th>
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
                  <button onClick={() => handleAbrirModalMostrar(proyecto.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(proyecto)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarProyecto(proyecto.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                </td>
              </tr>
            ))}
            {/* {asignacionesFiltrados.map((asignacion) => (
              <tr key={asignacion.id}>
                <td>{asignacion.id}</td>
                <td>{asignacion.nombre}</td>
                <td>{asignacion.proyectoID}</td>
                <td>{asignacion.fechaAsignacion}</td>
                <td className="acciones">
                  <button onClick={() => handleAbrirModalMostrar(asignacion.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(asignacion)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarAsignacion(asignacion.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                  <br />
                </td>
              </tr>
            ))} */}
          </tbody>
        </table>
      </div>
      <ModalAgregarAsignacion/>
      {/* <ModalAgregarAsignacion fetchAsignaciones={fetchAsignaciones} /> */}
      {/* <ModalEditarAsignacion idAsignacion={idAsignacionSeleccionado} fetchAsignaciones={fetchAsignaciones} />
      <ModalMostrarAsignacion idAsignacion={idAsignacionSeleccionado} /> */}
    </div>
  );
};

export default CRUDAsignaciones;