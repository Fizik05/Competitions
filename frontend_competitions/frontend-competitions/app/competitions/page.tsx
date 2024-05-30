"use client";

import Button from "antd/es/button/button"
import { Competitions } from "../components/Competitions";
import { useEffect, useState } from "react";
import { getAllCompetitions } from "../services/competitions";
import Title from "antd/es/skeleton/Title";
import { CreateUpdateCompetitions, Mode } from "../components/CreateUpdateCompetition";
import { request } from "http";
import { CompetitionRequest, createCompetition, deleteCompetition, updateCompetition } from "../services/competitions";


export default function Competitionspage(){
  const defaultValues = {
    name: "",
    description: ""
  } as Competition;

  const [values, setValues] = useState<Competition>({
      name: "",
      description: "",
  } as Competition);

  const [competitions,setCompetitions] = useState<Competition[]>([]);
  const [loading,setLoading] = useState(true);
  const[isModalOpen,setIsModalOpen] = useState(false);
  const[mode,setMode] = useState(Mode.Create);
  
  
  useEffect(()=>{
    const getCompetitions = async () => {
      const competitions = await getAllCompetitions();
      setLoading(false);
      setCompetitions(competitions);
    }; 

    getCompetitions();
  },[]);

  const handleCreateCompetition = async(request: CompetitionRequest) => {
    await createCompetition(request);
    closeModal();

    //const competition = await getAllCompetitions();
    //setCompetitions(competitions);
  }

  const handleUpdateCompetition = async (id:number, request: CompetitionRequest)=>{
    await updateCompetition(id, request);
    closeModal();

    //const competition = await getAllCompetitions();
    //setCompetitions(competitions);
    
  }

  const handleDeleteCompetition = async(id:number) => {
      await deleteCompetition(id);

  }

  const openModal = () => {
    //setMode(Mode.Create);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setValues(defaultValues);
    setIsModalOpen(false);
  };

  const openEditModal = (competition: Competition) => {
    setMode(Mode.Edit);
    setValues(competition);
    setIsModalOpen(true);
  }
  

  return(
    <div>
      <Button>Добавить соревнование</Button>
      
      <CreateUpdateCompetitions
          mode={mode} 
          values={values} 
          isModalOpen={isModalOpen} 
          handleCreate={handleCreateCompetition} 
          handleUpdate={handleUpdateCompetition}
          handleCancel={closeModal}
      />

      {loading ? (
      <Title>Loading....</Title>
      ): (
        <Competitions competitions={competitions} handleOpen={openEditModal} handleDelete={handleDeleteCompetition}/>
      )}
      
    </div>
  )
}