# IEC61850 wrapper API
IEC61850 wrapper API is developed to read parameters from the IED devices and write parameters to the IED devices using IEC61850 protocol this API is made in such way that we can read multipe varaible groups inlcuding  parameter value, quality and timestamp in a single request, it also has the feature to read the dataset, writig values and control object commands this library will work on windows and linux both platforms. <br />

Note: This library can run in any computer where docker is installed. 

# Reading steps:
###### 1. Reading multiple parameter groups. <br />

    API request method : GET

    API url : http://0.0.0.0:84/read/multiplegroups

    body input: {
            "ipaddress": "192.168.0.211",
            "port":102,
            "logicaldevicename":"PQMLD0",
            "varaddress":["M03_MMXU1$MX$PhV$phsA",
                        "M03_MMXU1$MX$TotW",
                        "M03_MMXU1$MX$TotVAr",
                        "M03_MMXU1$MX$TotVA",
                        "M03_MMXU1$MX$TotPF"
                    ]   
    }

###### 2. Read dataset values. <br />

    API request method : GET

    API url : http://0.0.0.0:84/read/dataset

    body input: {
                "ipaddress": "192.168.0.121",
                "port":102,
                "varaddress":"WAGOTestData/LLN0.Test"
            }

###### 3. Create dataset and read dataset values. <br />
    
    API request method : GET

    API url : http://0.0.0.0:84/read/multiplevariable

    body input: {
                "ipaddress": "192.168.0.72",
                "port":102,
                "varaddress":[
                                "WAGOF1/XGGIO1.Ind1.stVal[ST]",
                                "WAGOF1/XGGIO1.Ind1.t[ST]",
                                "WAGOF1/XGGIO1.Ind1.q[ST]",
                                "WAGOF2/XGGIO1.Ind1.stVal[ST]",
                                "WAGOF2/XGGIO1.Ind1.t[ST]",
                                "WAGOF2/XGGIO1.Ind1.q[ST]"
                            ]
            }
###### 4. Read single parameter group. <br /> 

    API method : GET

    API url : http://0.0.0.0:84/read/singlegroup

    body input: {
                "ipaddress": "192.168.0.118",
                "port":102,
                "fc":"ST",
                "varaddress":"AVR1TC230/GGIO1.GPO1"
            }

###### 5. Read only single paramter value <br />

    API method : GET

    API url : http://0.0.0.0:84/read/singlevariable

    body input: {
                "ipaddress": "192.168.0.118",
                "port":102,
                "fc":"ST",
                "datatype" :"BOOLEAN",
                "varaddress":"AVR1TC230/GGIO1.GPO1.stVal"
            }

###### 6. Control object commands write operation. <br />

    API method : POST

    API url : http://0.0.0.0:84/write/cmd

    body input: {
                "ipaddress": "192.168.0.121",
                "port":102,
                "operate":false,
                "varaddress":"AA1E1Q01KF1CTRL/CBCSWI1.Pos"
            }

###### 7. Write specific parameter value.  <br />

    API method : POST

    API url : http://0.0.0.0:84/write/value

    body input: `{
                "ipaddress": "192.168.0.121",
                "port":102,
                "varaddress":"WAGOTestData/GGIO1.AnIn.subMag.f",
                "fc":"SV",
                "newvalue":55.765
            }`

# Requirements:
###### 1. dotnet-sdk-3.1
###### 2. aspnetcore-runtime-3.1
###### 3. dotnet-runtime-3.1
###### 4. mongodb (for logs)
###### 5. docker (for logs)
###### 6. dcoker-compose (for logs)


# Installation steps 

###### 1. Installation without docker
a. `Clone` or `download` the repository. <br />
b. Give access `chmod +x requirements.sh`. <br />
c. Install required applications `./ requirements.sh` <br />
d. Run the API `dotnet run` <br />

###### 2. Installation with docker 
a. Install `docker`. <br />
b. Install `docker-compose`. <br />
c. Goto the project directory where your API is available `/device-wrapper-iec61850/`. <br />
d. Build docker image and run the app `docker-compose up --build -d`. <br />
