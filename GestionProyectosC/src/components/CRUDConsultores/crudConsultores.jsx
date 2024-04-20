import React, { useState, useEffect } from 'react';
import './crudConsultores.css';
import api from '../../api';
import ModalAgregarConsultor from './modalesC/modalAgregarConsultor';
import ModalEditarConsultor from './modalesC/modalEditarConsultor';
import ModalMostrarConsultor from './modalesC/modalMostrarConsultor';

const CRUDConsultores = () => {
  const [consultores, setConsultores] = useState([]);
  const [consultoresFiltrados, setConsultoresFiltrados] = useState([]);
  const [idConsultorSeleccionado, setIdConsultorSeleccionado] = useState(null);

  const fetchConsultores = async () => {
    try {
      const response = await api.get('/Consultor');
      setConsultores(response.data);
      setConsultoresFiltrados(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchConsultores();
  }, []);

  const handleSearch = (event) => {
    const searchTerm = event.target.value.toLowerCase();
    const filteredConsultores = consultores.filter((consultor) =>
      consultor.nombre.toLowerCase().includes(searchTerm) ||
      consultor.email.toLowerCase().includes(searchTerm) ||
      consultor.especialidad.toLowerCase().includes(searchTerm)
    );
    setConsultoresFiltrados(filteredConsultores);
  };

  const handleAbrirModalAgregar = () => {
    const modal = document.getElementById('modalAgregarConsultor');
    modal.showModal();
  };

  const handleAbrirModalEditar = (consultor) => {
    const modal = document.getElementById('modalEditarConsultor');
    modal.showModal();
    setIdConsultorSeleccionado(consultor.id);
  };

  const handleAbrirModalMostrar = (idConsultor) => {
    setIdConsultorSeleccionado(idConsultor);
    const modal = document.getElementById('modalMostrarConsultor');
    modal.showModal();
  };

  const handleEliminarConsultor = async (idConsultor) => {
    try {
      const response = await api.delete(`/Consultor/${idConsultor}`);
      console.log('Consultor eliminado:', response);
      fetchConsultores();
    } catch (error) {
      console.error('Error al eliminar consultor:', error);
    }
  };

  return (
    <div id="containerConsultores">
      <h1>Gestión de consultores</h1>
      <div id="controles">
        <button type="button" id="btnNuevoConsultor" onClick={handleAbrirModalAgregar}>
          <span className="material-symbols-outlined">add</span>
          <p>Agregar nuevo Consultor</p>
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
              <th>Especialidad</th>
              <th>Correo electrónico</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {consultoresFiltrados.map((consultor) => (
              <tr key={consultor.id}>
                <td>{consultor.id}</td>
                <td>{consultor.nombre}</td>
                <td>{consultor.especialidad}</td>
                <td>{consultor.email}</td>
                <td className="acciones">
                  <button onClick={() => handleAbrirModalMostrar(consultor.id)}>
                    <span className="material-symbols-outlined visibility">visibility</span>
                  </button>
                  <button onClick={() => handleAbrirModalEditar(consultor)}>
                    <span className="material-symbols-outlined edit">edit</span>
                  </button>
                  <button onClick={() => handleEliminarConsultor(consultor.id)}>
                    <span className="material-symbols-outlined delete">delete</span>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <ModalAgregarConsultor fetchConsultores={fetchConsultores} />
      <ModalEditarConsultor idConsultor={idConsultorSeleccionado} fetchConsultores={fetchConsultores} />
      <ModalMostrarConsultor idConsultor={idConsultorSeleccionado} />
    </div>
  );
};

export default CRUDConsultores;