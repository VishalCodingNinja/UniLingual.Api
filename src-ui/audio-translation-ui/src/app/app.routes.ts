import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SendToTeamsComponent } from './components/send-to-teams/send-to-teams.component';
import { CustomTextToSpeechComponent } from './components/custom-text-to-speech/custom-text-to-speech.component'; // Updated import
import { RecognizeSpeechComponent } from './components/recognize-speech/recognize-speech.component';
import { TranslateTextComponent } from './components/translate-text/translate-text.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'send-to-teams', component: SendToTeamsComponent },
    { path: 'custom-text-to-speech', component: CustomTextToSpeechComponent }, // Updated route
    { path: 'recognize-speech', component: RecognizeSpeechComponent },
    { path: 'translate-text', component: TranslateTextComponent },
    { path: 'text-to-speech', component: TranslateTextComponent },
];
