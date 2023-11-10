import {
  ApertureIcon,
  CopyIcon,
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
    to: '/dashboard',
    auth: true
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