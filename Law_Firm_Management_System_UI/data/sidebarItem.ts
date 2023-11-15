import {
  BriefcaseIcon, HistoryIcon, ClipboardTextIcon, UserCogIcon, CalendarIcon,
  QuestionMarkIcon, ApertureIcon, CopyIcon,
  LayoutDashboardIcon, MoodHappyIcon, TypographyIcon
} from 'vue-tabler-icons';

export interface menu {
  header?: string;
  title?: string;
  icon?: any;
  to?: string;
  chip?: string;
  chipColor?: string;
  chipVariant?: string;
  chipIcon?: string;
  children?: menu[];
  disabled?: boolean;
  type?: string;
  subCaption?: string;
  auth?: boolean;
}

const sidebarItem: menu[] = [
  { header: 'Home' },
  {
    title: 'Dashboard',
    icon: LayoutDashboardIcon,
    to: '/home/dashboard',
    auth: true,
  },
  {
    title: 'Appointment',
    icon: CalendarIcon,
    to: '/home/appointment',
  },
  {
    title: 'Management',
    icon: BriefcaseIcon,
    children: [
      {
        title: 'Case',
        icon: BriefcaseIcon,
        to: '/case',
      },
      {
        title: 'Case History',
        icon: HistoryIcon,
        to: '/case/history',
      },
      {
        title: 'Task',
        icon: ClipboardTextIcon,
        to: '/task',
      },
      {
        title: 'Task History',
        icon: HistoryIcon,
        to: '/task/history',
      },
      {
        title: 'Event',
        icon: HistoryIcon,
        to: '/event',
      },
    ]
  },
  { header: 'Others' },
  {
    title: 'Question & FAQ',
    icon: QuestionMarkIcon,
    to: '/others/question',
  },
  { header: 'Configuration' },
  {
    title: 'User',
    icon: UserCogIcon,
    to: '/configuration/user',
  },
  { header: 'Sample Page' },
  {
    title: 'Sample Page',
    icon: ApertureIcon,
    to: '/sample-page',
    auth: true
  },
  {
    title: 'Typography',
    icon: TypographyIcon,
    to: '/sample-page/typography'
  },
  {
    title: 'Shadow',
    icon: CopyIcon,
    to: '/sample-page/shadow'
  },
  {
    title: 'Icons',
    icon: MoodHappyIcon,
    to: '/sample-page/icons'
  },
];

export default sidebarItem;