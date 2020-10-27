window.onload = function () {  
    const vm  = new Vue({
        el:'#vue-app',
        data:{
            results:Object,
            Employees:[],
            EmpData:{
                FirstName:"",
                Lastname:"",
                EmailID:"",
                Address:"",
                Gender:"",
                DOB:"",
                Designation:""
            }
        },
    
    
        mounted() {
            //axios.post("https://epay.spectra.co/mwapisales/index.php", {
                //params: this.axiosParams
            //})
            //.then(response => {this.results = response.data.results})
            //.then(response => {
                //console.log("Response:", response.data.errormsg);
                //alert(response.data.errormsg);
            //})        
            //.catch(error => {
                //console.log(error);
            //}) 
            
            
        },

        computed: {
            axiosParams() {
                const params = new URLSearchParams();
                params.append('Authkey', 'AdgT6SnjAkeLqlkE4');
                params.append('Action', 'resendOTP');
                params.append('CAFNumber', 'CAF_H_25');
                params.append('userName', 'IN\\crm.deploy');
                params.append('password', 'Crdm@311#');
                return params;
            }
        },
        methods: {
            GetResult: function() {
                axios.get("/Employee/GetEmployeeList")
                .then(response => {
                    this.Employees=response.data;
                    //debugger;
                    //console.log("Response:", response.data);                
                })
                .catch(response => {
                    console.log("Error:", response);
                })
            },

            PostEmpData:function(){
                console.log(this.EmpData.FirstName);
                axios.post('https://localhost:44373/Employee/InsertUpdateEmployee', 
                {                   
                    firstName: this.EmpData.FirstName, 
                    lastname: this.EmpData.LastName,
                    emailID: this.EmpData.EmailID,
                    gender: this.EmpData.Gender,
                    designation: this.EmpData.Designation,
                    dOB: this.EmpData.DOB,
                    address: this.EmpData.Address                     
                   
                })
                .then(response => {
                    if(response.data.status === true){
                        alert(response.data.successMsg);
                    }
                    else if(response.data.status === false){
                        alert(response.data.errorMsg);
                    }                    
                })                
                .catch(error => {});
            },

            goEmpDetail: function (EmpID) {                
                window.location.href = "/Employee/Detail?EmpID=" + EmpID; 
            }
        }    
    });
}