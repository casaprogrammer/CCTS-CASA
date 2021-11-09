# CCTS-CASA
Consolidated Cane Tracking System

A Desktop based application used to track Sugar canes dumped into the cane carrier going to the NIR (Near Infrared Spectrometer) 
for scanning. 

Basically, it tracks the position of the Sugar cane on 3 different stages of cane preparation namely:
1. Side cane carrier
2. Main cane carrier
3. Cane Knives
4. Shredder

To track the position, the application is dependent on the number of revolution made by the sprockets. 
The specific countings of the revolution to determine the position of the Sugar cane, 
is determined through visually observing how many revolution would it take for the Sugar Cane to transfer on to the next 
cane carrier area. 

To track the number of revolutions on each cane carriers, metal proximity sensors are use near the sprockets which
triggers the relay every time the sprocket's hand pass by the sensors. These relays are then connected to an 
Arduino that sends the data to the PC hosting the application.

