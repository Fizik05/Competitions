export interface CompetitionRequest{
  name:	string
  description:	string
}

/*export const getAllCompetitions = async()=>{
  const response = await fetch("https://localhost:7036/api/Competitions");

  return response.json();
};
*/
export const createCompetition = async (competitionRequest: CompetitionRequest) => {
  await fetch("https://localhost:7036/api/Competitions",{
    method:"POST",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify(competitionRequest),
  });
};

export const updateCompetition = async (id: string, competitionRequest: CompetitionRequest) => {
  await fetch('https://localhost:7036/api/Competitions/${id}',{
    method:"PUT",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify(competitionRequest),
  });
}

export const  deleteCompetition = async (id: string) => {
  await fetch('https://localhost:7036/api/Competitions/${id}',{
    method:"DELETE"
  });
}

