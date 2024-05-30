import Card  from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button"

interface Props{
  competitions:Competition[]
}

export const Competitions= ({competitions}:Props) => {
  return (
    <div className="cards">
      {competitions.map((competition : Competition) => (
        <Card
          key={competition.id} 
          title={<CardTitle name={competition.name} description={competition.description}/>}
          bordered ={false}
        >
          <p>{competition.description}</p>
          <div className="card_buttons">
            <Button>
              Edit
            </Button>
            <Button>
              Delete
            </Button>
          </div>
        </Card>
      ))}
    </div>
  )
}