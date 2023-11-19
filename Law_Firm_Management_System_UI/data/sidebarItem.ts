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
  },
  {
    title: "Cases",
    icon: tablerIcon.BriefcaseIcon,
    to: "/cases",
  },
  {
    title: "Tasks",
    icon: tablerIcon.ClipboardTextIcon,
    to: "/tasks",
  },
  {
    title: "Events",
    icon: tablerIcon.CalendarIcon,
    to: "/events",
  },
  {
    title: "Documents",
    icon: tablerIcon.FileIcon,
    to: "/documents",
  },
  {
    title: "Contacts",
    icon: tablerIcon.AddressBookIcon,
    children: [
      {
        title: "Clients",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/clients",
      },
      {
        title: "Paralegals",
        icon: tablerIcon.UsersGroupIcon,
        to: "/contacts/paralegals",
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
      },
      {
        title: "Firm Settings",
        icon: tablerIcon.BuildingIcon,
        to: "/configuration/firm-settings",
      },
      {
        title: "Security",
        icon: tablerIcon.ShieldLockIcon,
        to: "/configuration/security",
      },
    ]
  },
  {
    title: "Help & Support",
    icon: tablerIcon.HelpIcon,
    to: "/help-and-support",
  },
  { header: "Sample" },
  {
    title: "Sample Page",
    icon: tablerIcon.ApertureIcon,
    to: "/sample",
    auth: true
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