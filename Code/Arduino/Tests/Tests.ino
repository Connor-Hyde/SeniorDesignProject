const int stepPin1 = 11;
const int dirPin1 = 13;

const int del = 15;
void setup() {
  // put your setup code here, to run once:
  pinMode(stepPin1,OUTPUT); 
  pinMode(dirPin1,OUTPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(dirPin1,HIGH); // Enables the motor to move in a particular direction

  //delay(del); 

  // Makes 200 pulses for making one full cycle rotation
    for(int x = 0; x < 125; x++) {
      digitalWrite(stepPin1,HIGH);
      //delay(del); 
      digitalWrite(stepPin1,LOW);
      delay(del); 
    }
    delay(1000);
  digitalWrite(dirPin1, LOW);
  for(int x = 0; x < 125; x++) {
    digitalWrite(stepPin1,HIGH);
    //delay(del); 
    digitalWrite(stepPin1,LOW);
    delay(del); 
  }
    delay(1000);

  //delay(100);
}
