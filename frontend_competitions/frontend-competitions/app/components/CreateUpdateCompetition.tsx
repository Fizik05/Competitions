import { Input, Modal } from "antd";
import { CompetitionRequest } from "../services/competitions";
import { title } from "process";
import { useState } from "react";
import TextArea from "antd/es/input/TextArea";

interface Props{
  mode: Mode;
  values: Competition;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleCreate: (request: CompetitionRequest) => void;
  handleUpdate: (id:number, request: CompetitionRequest) => void; 
}
export enum Mode{
  Create,
  Edit,

}

export const CreateUpdateCompetitions = ({
  mode, 
  values,
  isModalOpen,
  handleCancel,
  handleCreate,
  handleUpdate,
}: Props) => {

  const [name,setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  
  const handleOnOk = async() => {
    const CompetitionRequest = {name, description};

    mode == Mode.Create
    ? handleCreate(CompetitionRequest) 
    : handleUpdate(values.id, CompetitionRequest)
  }
  
  return (
    <Modal
      name={
        mode===Mode.Create ? "Добавить соревнование" : "Редактировать соревнование"
      } 
      open={isModalOpen} 
      onOk = {handleOnOk}
      onCancel = {handleCancel}
      cancelText={"Отмена"}
    >
      <div className="competition_modal">
        <Input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Название"
        />
        <TextArea
          value={description}
          onChange={(e) => setName(e.target.value)}
          autoSize = {{minRows: 3, maxRows: 3}}
          placeholder="Описание"
        />
      </div>
    </Modal>
  )
};
