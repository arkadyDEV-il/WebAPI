import React from 'react';
import './AutoCompleteText.css' 
import Employee from './Employee';

export default class AutoCompleteText extends React.Component{
    constructor(props){
        super(props);
        this.items = [
            'David', 'Damien', 'Sara', 'Jane', 'John', 'George', 'Paul',
            'Sam', 'Phil', 'Leroy', 'Jianna','Arkady','Mike','Chuck',
            'Mickey'
        ];
        this.state = {
                suggestions:[],
                text:'',
                chosen:{
                    "EmployeeId":"",
                    "EmployeeName":""
                    ,"PhotoFileName":"",
                    "Position":""
                }
        };

    }
    
onTextChanged = (e) => {
    const value = e.target.value;
    let suggestions = []
    if(value.length>1){
        //const regex = new RegExp(`^${value}`,'i');
        //suggestions = this.items.sort().filter(v => regex.test(v));
        fetch('http://localhost:5000/api/employee/GetEmployee/' + value)
        .then(response=>response.json())
        .then(data=>{
            this.setState({suggestions:data,text:value});
        })
    }
    else{
        this.setState({suggestions:[],text:value});
        console.log( this.state.text);
    }
    
        
    
}

suggestionSelected(emp) {
    this.setState(()=> ({
        text:emp.EmployeeName + ' ' + emp.Position,
        suggestions: [],
        chosen:emp
    }));
    
}

renderSuggestions(){
    const {suggestions } = this.state;
    if(suggestions.length === 0){
        return null;
    }

    return (
    <ul>
         {suggestions.map((emp)=> 
            <li key={emp.EmployeeId} onClick={()=> this.suggestionSelected(emp)}>
                {emp.EmployeeName + ' ' + emp.Position}
            </li>)}
    </ul>)
    
}

callEmployee(){
    return (
        <Employee employee={this.state.chosen}/>
    )
}

    render(){
        const {text } = this.state;
        return(
        <div>
        <div className='AutoCompleteText'>
            <input value={text} onChange={this.onTextChanged} type="text"/>
            {this.renderSuggestions()}
        </div>
            <div>
                {this.callEmployee()}
            </div>
        </div>)
    }
}