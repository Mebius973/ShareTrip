import PrimaryButton from '../PrimaryButton/PrimaryButton'
import SecondaryButton from '../SecondaryButton/SecondaryButton'
import Title from '../Title/Title'
import './WelcomeHeader.css'

type WelcomeHeaderProps = {
  login: () => void
  register: () => void
}

function WelcomeHeader(props: WelcomeHeaderProps) {
  return (
    <div className="welcome-header-container">
      <div className="welcome-header-content">
        <div className="welcome-header-title">
          <Title />
        </div>
        <div className="welcome-header-button-container">
          <PrimaryButton text="Login" action={props.login} />
          <SecondaryButton text="Register" action={props.register} />
        </div>
      </div>
    </div>
  )
}

export default WelcomeHeader
