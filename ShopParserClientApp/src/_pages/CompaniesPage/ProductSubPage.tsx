import {
  Grid,
} from "@material-ui/core";
import React, { useEffect } from "react";
import { useParams } from "react-router-dom";



export const ProductSubPage: React.FC = () => {

  const { companyId, categoryId } = useParams<Record<string, string | undefined>>();
  

  useEffect(() => {
        
    return () => {
      
    }
  }, [])
  return (
    <Grid item >
      <div>
        <h1>{companyId}</h1>
        <h2>{categoryId}</h2>
      </div>
</Grid>);
    
  
};