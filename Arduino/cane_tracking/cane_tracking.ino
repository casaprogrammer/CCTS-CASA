const int shreddedPin = 5;
const int caneKnivesPin = 4;
const int mainCanePin = 3;
const int sideCanePin = 2;

int shreddedState = 0;
int shreddedCurrentState = 0;

int caneKnivesState = 0;
int caneKnivesCurrentState = 0;

int mainCaneState = 0;
int mainCaneCurrentState = 0;

int sideCaneState = 0;
int sideCaneCurrentState = 0;

void setup() {
  pinMode(shreddedPin, INPUT_PULLUP);
  pinMode(caneKnivesPin, INPUT_PULLUP);
  pinMode(mainCanePin, INPUT_PULLUP);
  pinMode(sideCanePin, INPUT_PULLUP);
  Serial.begin(9600);
}

void loop() {
  int shreddedSensor = digitalRead(shreddedPin);
  int caneKnivesSensor = digitalRead(caneKnivesPin);
  int mainCaneSensor = digitalRead(mainCanePin);
  int sideCaneSensor = digitalRead(sideCanePin);

  shreddedCane(shreddedSensor);
  caneKnives(caneKnivesSensor);
  mainCane(mainCaneSensor);
  sideCane(sideCaneSensor);
}

void shreddedCane(int pin) {
  if (pin == HIGH) {
   shreddedCurrentState = 0;
    if (shreddedCurrentState != shreddedState) {
      Serial.println("No Object");
      shreddedState = 0;
      delay(5);
    }
  }
  else {
    shreddedCurrentState = 1;
    if (shreddedCurrentState != shreddedState) {
      Serial.println("Shredded Cane Object");
      shreddedState = 1;
      delay(5);
    }
  }
}

void caneKnives(int pin) {
  if (pin == HIGH) {
    caneKnivesCurrentState = 0;
    if (caneKnivesCurrentState != caneKnivesState) {
      Serial.println("No Object");
      caneKnivesState = 0;
      delay(5);
    }
  }
  else {
    caneKnivesCurrentState = 1;
    if (caneKnivesCurrentState != caneKnivesState) {
      Serial.println("Cane Knives Object");
      caneKnivesState = 1;
      delay(5);
    }
  }
}

void mainCane(int pin) {
  if (pin == HIGH) {
     mainCaneCurrentState = 0;
    if (mainCaneCurrentState != mainCaneState) {
      Serial.println("No Object");
      mainCaneState = 0;
      delay(5);
    }
  }
  else {
    mainCaneCurrentState = 1;
    if (mainCaneCurrentState != mainCaneState) {
      Serial.println("Main Cane Object");
      mainCaneState = 1;
      delay(5);
    }
  }
}

void sideCane(int pin) {
  if (pin == HIGH) {
   sideCaneCurrentState = 0;
    if (sideCaneCurrentState != sideCaneState) {
      Serial.println("No Object");
      sideCaneState = 0;
      delay(5);
    }
  }
  else {
    sideCaneCurrentState = 1;
    if (sideCaneCurrentState != sideCaneState) {
      Serial.println("Side Cane Object");
      sideCaneState = 1;
      delay(5);
    }
  }
}
