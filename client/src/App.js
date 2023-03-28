
import React, {useState} from "react";
import Header from "./pages/login/index";

export default  function App() {
  const [counter, setCounter] = useState(0); 
  //Array [valor, ChangeValueFunciton]
  function Increment(){
    setCounter(counter+1);
  }

  return(
    <div>
      <Header>
      Counter: {counter}
    </Header>

    <button onClick={Increment}>Add</button>

    </div>
  );
}

