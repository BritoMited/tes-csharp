// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;

int[] vetor = new int[100];
bool troca;
Random rando = new Random();

for (int i = 0; i < vetor.Length; i++)
{
    vetor[i] = rando.Next(100);

    Console.Write(" " + vetor[i]);      
}


do {
    troca = false;
    for (int i = 0; i < vetor.Length -1; i++){
       if(vetor[i] > vetor[i+1]){

        int aux = vetor[i];
        vetor[i] = vetor[i+1];
        vetor[i+1] = aux;
        troca = true;

       } 

    }
    
}while(troca == true);

Console.Write("\n\n\n");      

for (int i = 0; i < vetor.Length; i++)
{
    Console.Write(" " + vetor[i]);
}