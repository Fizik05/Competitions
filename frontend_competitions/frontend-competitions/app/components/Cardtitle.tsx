interface Props{
  title: string;
  description: string;
}

export const CardTitle = ({title,description}:Props) => {
  return(
    <div style={{
      display: "flex",
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
    }}>
      <p className="card_title">{title}</p>
      <p className="card_description">{description}</p>
    </div>
  )
}