var  Player : Transform;
var MoveSpeed = 4; //speed of enemies
var MaxDist = 10; // aggro range
var Difficulty = 1;// difficulty multiplier ie gets harder over time

Function Start(){}

Function Update()
{
    transform.LookAt(Player); // enemies will always look at player

    if(Vector3.Distance(transform.position, Player.position)<= MaxDist){

        transform.position += transform.forward*MoveSpeed*Time.deltaTime*Difficulty;
    }  
}
