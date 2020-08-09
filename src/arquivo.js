const readline = require('readline') //permite ler arquivo linha por linha
const fs = require ('fs')            //permite pegar um arquivo local


var assemblyFile = readline.createInterface({
    input : fs.createReadStream('script.txt'), //readalbe
    output: process.stdout,                    //writeable
    terminal: false                            //n duplica a saida, ja que dps tem um console.log
})

assemblyFile.on('line',function(line){
    console.log(line) 

   assemblyFile.close(); // n exibi a ultima linha do arquivo
   
})