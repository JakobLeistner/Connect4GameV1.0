import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { APIInterface } from '../../RestAPIClient/APIInterface';
import { MyPlayer, RegisterPlayerResponse } from '../../RestAPIClient/Contracts/Contracts';
import { PlayerHolder } from '../../RestAPIClient/Services/PlayerHolder';
import { SignalRService } from '../../RestAPIClient/Services/SignalRService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent 
{
  RegisterButton = "Register";
  RandomNameButton = "Help";
  myPlayer = this.playerHolder.myPlayer as MyPlayer;

  constructor(private playerHolder: PlayerHolder, private apiInterface: APIInterface, public signalRService: SignalRService, public router: Router)
  { }

  public RegisterPlayer()
  {
    const InputName = (document.getElementById("InputName") as HTMLInputElement).value;
    this.apiInterface.RegisterPlayer(InputName, this.signalRService.connectionId).subscribe
    ({
      next: (response: RegisterPlayerResponse) => { 
        
        this.playerHolder.SetPlayer(response.registeredPlayer);  
        this.router.navigate(['/queue']);
      
      },
      error: (error: any) => {console.error(error);},
      complete: () => {}
    });
  }

  public GenerateRandomName()
  {
    var names = ['Verity Adkins', 'Kaylee Petty', 'Richard Stuart', 'Solomon Dotson', 'Naima Thomas', 'Ross Paul', 'Elinor Marshall', 'Troy Pacheco', 'Zakariya Harding', 'Marshall Mack', 'Shane Mendoza', 'Lia Nielsen', 'Virginia Lucas', 'Samir Potts', 'Tyler Dawson', 'Rachel Patterson', 'Olly Bean', 'Eshal Willis', 'Jan Meadows', 'Mohammad Harrington', 'Oskar Schroeder', 'Isobella Marsh', 'Faiza Warren', 'Amin Ferrell', 'Malika Walters', 'Issac Harrison', 'Kelvin Dyer', 'Nettie Hooper', 'Jaya Pope', 'Kyan Herrera', 'Wilson Holloway', 'Tatiana Whitney', 'Safia RushB', 'Stanley Berry', 'Tanisha McLaughlin', 'Alexis French', 'Saoirse Foster', 'Ezekiel Gutierrez', 'Edith McBride', 'Colin Holt', 'Maxim Myers', 'Gail Carson', 'Orla Wilkins', 'Anoushka Blackburn', 'Anthony Castaneda', 'Carol Blankenship', 'Ellie Berg', 'Lydia Rose', 'Elle Chaney', 'Aya Beck', 'Sarah Lozano', 'Captain Lightning', 'Shadow Bat', 'Doctor Mercury', 'Arachno Hex', 'Ruby Cranium', 'Giga Bane', 'Mirror Mask', 'Doctor Flamingo', 'Arachno Shadow', 'Uber Shadow', 'Indigo Dazzler', 'Web Fly', 'Sensational Thinker', 'Super Bane', 'Sting Mask', 'Hyper Bane', 'Super Hex', 'Saturn Shadow', 'Nuclear Mask', 'Giga Hex', 'Beast Bane', 'Multiple Thinker', 'Wonderful Bite', 'Warp Shadow', 'Web Flame', 'Flamingo Mask', 'Flaming Shadow', 'Silver Sirloin', 'Quantum Mask', 'Arachno Spider', 'Amazing Howler', 'Wonder Cat', 'Ultra Tiger', 'Flea Shadow', 'Neon Shadow', 'Dream Flame', 'Astounding Wolf', 'Diamond Fly', 'Warp Mask', 'Wolf Cat', 'Ghost Bane', 'Dark Cat', 'Star Puma', 'Commander Devil', 'Skeleton Shadow', 'Hyper Mask', 'Doctor Venus', 'Amazing Jewel', 'Uber Braid', 'Hydro Flame', 'Professor Machine', 'Indigo Hawk', 'Gold Prism', 'Professor Ultra', 'Diamond Flame', 'Astounding Jaws', 'Machine Mask', 'Giga Thinker', 'Awesome Braid', 'Ghost Mask', 'Whispering Mask', 'Storm Hex', 'Silver Web', 'Shadow Puma', 'Commander Giga', 'Temporal Skull', 'Meat Cat', 'Steel Shadow', 'Sensational Prism', 'Alexander Marcus', 'Hyper Stone', 'Proton Terror', 'Skull Spider', 'Doctor of Screams', 'Proton Nasty', 'Professor of Rats', 'Captain Peculiar', 'Doctor of Storms', 'Wolf Parasite', 'Commander of Mantis', 'Commander of Madness', 'Electroid Nasty', 'Professor of Rats', 'Screaming Parasite', 'Razor Death Lord', 'Cackling Web', 'Proton Vanquisher', 'Indigo Bruiser', 'Commander of Nightmares', 'Commandant Demon', 'Death Storm', 'Colonel Fistpuncher', 'Cypher Death Lord', 'Laughing Thinker', 'Darkstorm Mangler', 'Terrible Scorpio', 'Commander Phantom', 'Professor of Terror', 'Neuro Volt', 'Bane Mighty', 'Copper Devastation', 'Lake Heron', 'Skinny Harley', 'Short Mediated', 'Devan Ninja', 'Ash under the sea', 'Camel Mammal', 'Sassy Keenan', 'Western Lovelace', 'Jack Sparrow', 'Harley Diamond', 'Waverley Moon', 'Mean but Fair', 'Jalen Smith', 'Halley Splatter', 'Keelan Glow', 'Legal Whitley', 'Gloom Blair', 'Lustrous Darby', 'Mighty Ricky', 'Neon Winter', 'Avalon Exotic', 'Stew Rabbit', 'Consistant Disaster', 'Crystal Deer', 'Marian Koala', 'Brave Paris', 'Hadley Sequoia', 'Clockwork Damnation', 'Lucent Sartre', 'Skinny Marquis', 'Artful Kelby', 'Arch March', 'Harvest Carebear', 'Mini Walnut', 'Piper Square', 'Rapid Lindy', 'Lime Gunner', 'Zinc Square', 'Carmens Bart Kese', 'Fair Winner', 'Zinedine Zidane', 'Teagan Feverdream', 'Pale Dale', 'Rising Giraffe', 'Syrup Albino', 'Sublime Goblin', 'Earthly Terminator', 'Fair Galley', 'Brown Kim', 'Logan Deadwolf', 'Carter Sartre', 'Sage Stag', 'Jaiden Sorrowful', 'Vick Whitesnow', 'Baby Damnation', 'Kerry Claw', 'Morgan Seasnake', 'Neo Goldenchild', 'Sage Morpheus', 'Lee Vega', 'Cadet Vader', 'Angel Cosmonaut', 'Halley Neptune', 'Major Hydra', 'Doctor Tesla', 'Captain Darklight', 'Dax Huxley', 'Winter Cosmo', 'Halley Ray Gun', 'Emerson Einstein', 'Calvin Tull', 'Buzz Frankenstein', 'Freddie Sparks', 'Titus Sparks', 'Bucky Turing', 'Buzz Arkwright', 'Jake Flynn', 'Petey Torrington', 'Brantley Newton', 'Zoe Steel', 'Molly Sparks', 'Paige Dyson', 'Isabella Dyson', 'Daisy Babbage', 'Judy Heisenberg', 'Rosie Flynn', 'Violet Cavor', 'Vivian Hammerstein', 'Saramil Wilson', 'Heherson Jose', 'Kidlat Aguinaldo', 'Pacifico Peralta', 'Joselito Domingo', 'Lilibeth Ortega', 'Divina Custodio', 'Nenita Pasion', 'Cherry Andres', 'Maritess Manalo', 'Nar', 'Dis', 'Thror', 'Fror', 'Farin', 'Oin', 'Fili', 'Ibun', 'Azaghal', 'Fundin', 'Sam Laureate', 'Vesh Flanders', 'Beno February', 'Tor Fry', 'Darcy Archer', 'Hanzi Wood', 'Caesar Whitlock', 'Beno Rowan', 'Samael West', 'Zedkiel Moor', 'Dodinas Maggor', 'Saradas Clayhanger', 'Otho Brockhouse', 'Tolman Boffin', 'Otho Gawkroger', 'Hending Chubb', 'Isembard Bunce', 'Wiseman Bophin', 'Hildigrim Diggle', 'Posco Noakes', 'Professor Ice', 'Bloody Pulse', 'Arachno Storm', 'Darkstorm Terror', 'Neuro Terror', 'Captain Plutonium', 'Dr. Occult', 'Proton Pulse', 'Duke Terror', 'Master of Delight', 'Blood Fist', 'Ice Bullet', 'General Arachno', 'Howling Puppeteer', 'Doctor Atom', 'Lady of Storms', 'Bane Bonesmasher', 'Nasty Elecdroid ', 'Doctor of Rats', 'Black Slam', 'Bigfoot', 'Tiny Monkey', 'Da Vinci', 'Obiwan', 'R2', 'Yoda', 'Jack van Gogh', 'Xenomorph', 'Car Crash', 'Sociopath', 'Nerd Face', 'Lieutenant Kryten', 'Timo Tesla', 'General Stardust', 'Prof. Shroud', 'Evil Giga Bolt', 'Bronze Scrambler', 'Ninja Dagger', 'Vanilla L', 'DJ Slim Ali', 'Ill Needlz', 'Ya Problem', 'Spice Blade', 'Cold Dollar', 'Glock Glocc', 'Lake Freezer', 'Shoe Breaker', 'Stolen Win', 'Scary Gold', 'Pepa', 'Cyxa', 'Machste Nyx', 'Allmighty Hades', 'Horrible Owl', 'Doctor Atrocious', 'Agent Alarming', 'Gigantic Savage', 'Iron Cheetah', 'Old Warrior', 'Macho Ox', 'Doctor Unknown', 'Scarved Bandit', 'Captain Bitter', 'Flash Ninja', 'Razor Claw', 'Doctor Chievous', 'Mute Master', 'Light Stealer', 'Water Killer', 'Delirious Spy', 'Wicked Moth', 'Giga Wolf', 'Ibun Ion', 'Darkness Spreader', 'Mysterious Assassin', 'Undefeated Shadow', 'Golden Fool', 'Incredible Hornet', 'Polterblech', 'Lazy Savare', 'Professor Brainwave', 'Planet Spider', 'Death Star', 'Meerkat of Death', 'Black Web', 'Bloody Pyro', 'Black Thinker', 'Power Star', 'Professor of Pain', 'Fearsome Puppeteer', 'Dark Creature', 'Professor of Pain', 'Commandant Hypno', 'Professor of Madness', 'Proton Titan', 'Terrible Bruiser', 'Flaming Blazer', 'Bane the Awful', 'Terrible Spark', 'Arachno Scream', 'Maniacal Killer', 'Fearless Tess', 'Pologonic Square', 'Techno Player', 'Laughing Lava', 'Roaring Ripper', 'Zero Number', 'Icy Flame', 'Dark Snow', 'Bright Night', 'Nightmare Bringer', 'Nukable Nade', 'Shrieking Gargoyle', 'Anger of Mice', 'Lightning Slam', 'Fire Fist', 'Magno Spike', 'Fire of Enlightment', 'Bullet Morph', 'Fearsome Knight'];
    var rndInt = Math.floor(Math.random() * 400);
    (document.getElementById("InputName") as HTMLInputElement).value = names[rndInt];
  }

}


