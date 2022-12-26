import { IndustryVM } from './IndustryVM';
import { LocationVM } from './LocationVM';
import { TicketVM } from './TicketVM';

export interface ExpoVM {
  id: number;
  title: string;
  description: string;
  dateOfOpening: Date;
  dateOfClosing: Date;
  workHoursOpening: number;
  workHoursClosing: number;
  hasExpired: boolean;
  organizer: string;
  location: LocationVM;
  industry: IndustryVM;
  ticketTypes: TicketVM[];
  show:boolean;
}
