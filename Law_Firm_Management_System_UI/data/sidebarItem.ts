import * as tablerIcon from "vue-tabler-icons";

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
  { header: "Home" },
  {
    title: "Dashboard",
    icon: tablerIcon.LayoutDashboardIcon,
    to: "/dashboard",
    auth: true,
  },
  {
    title: "Appointments",
    icon: tablerIcon.Book2Icon,
    to: "/appointments",
    auth: true,
  },
  {
    title: "Cases",
    icon: tablerIcon.BriefcaseIcon,
    to: "/cases",
    auth: true,
  },
  {
    title: "Tasks",
    icon: tablerIcon.ClipboardTextIcon,
    to: "/tasks",
    auth: true,
  },
  {
    title: "Events",
    icon: tablerIcon.CalendarIcon,
    to: "/events",
    auth: true,
  },
  {
    title: "Documents",
    icon: tablerIcon.FileIcon,
    to: "/documents",
    auth: true,
  },
  {
    title: "Contacts",
    icon: tablerIcon.AddressBookIcon,
    children: [
      {
        title: "Clients",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/clients",
        auth: true,
      },
      {
        title: "Paralegals",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/paralegals",
        auth: true,
      },
    ]
  },
  {
    title: "Configuration",
    icon: tablerIcon.SettingsIcon,
    children: [
      {
        title: "User Settings",
        icon: tablerIcon.UserCogIcon,
        to: "/configuration/user-settings",
        auth: true,
      },
    ]
  },
  {
    title: "Legal Information",
    icon: tablerIcon.HelpIcon,
    to: "/legal-information",
    auth: true,
  },
  { header: "Sample" },
  {
    title: "Sample Page",
    icon: tablerIcon.ApertureIcon,
    to: "/sample",
  },
  {
    title: "Typography",
    icon: tablerIcon.TypographyIcon,
    to: "/sample/typography"
  },
  {
    title: "Shadow",
    icon: tablerIcon.CopyIcon,
    to: "/sample/shadow"
  },
  {
    title: "Icons",
    icon: tablerIcon.MoodHappyIcon,
    to: "/sample/icons"
  },
];

export default sidebarItem;